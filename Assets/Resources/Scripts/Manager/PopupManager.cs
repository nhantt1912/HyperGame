using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopupEntry
{
    public PopupType type;
    public GameObject prefab;
}

public enum PopupType
{
    Setting,
    Win,
    // add more types here
}

/// <summary>
/// PopupManager: lightweight manager to show/close popups by type.
/// - Map PopupType -> prefab via inspector (popupPrefabs)
/// - Instantiates prefabs under popupRoot (default: first Canvas found)
/// - Keeps optional instance reuse and a simple stack to handle modal order
/// </summary>
public class PopupManager : Singleton<PopupManager>
{
    [Header("Popup Root (Canvas)")]
    [SerializeField] private Transform popupRoot;

    [Header("Map popups to prefabs")]
    [SerializeField] private List<PopupEntry> popupPrefabs = new List<PopupEntry>();

    private Dictionary<PopupType, GameObject> _prefabMap;
    private Dictionary<PopupType, UIBase> _instanceMap = new Dictionary<PopupType, UIBase>();
    private Dictionary<PopupType, bool> _isSceneInstanceMap = new Dictionary<PopupType, bool>();
    private Stack<UIBase> _stack = new Stack<UIBase>();

    protected override void Awake()
    {
        base.Awake();
        BuildMap();
        if (popupRoot == null)
        {
            var canvas = FindObjectOfType<Canvas>();
            popupRoot = canvas != null ? canvas.transform : this.transform;
        }
    }

    private void BuildMap()
    {
        _prefabMap = new Dictionary<PopupType, GameObject>();
        foreach (var e in popupPrefabs)
        {
            if (e != null && e.prefab != null)
                _prefabMap[e.type] = e.prefab;
        }
    }

    /// <summary>
    /// Show a popup by type. Prefab must be assigned in inspector.
    /// If reuseIfExists is true and an instance exists, it will be reused.
    /// </summary>
    public UIBase Show(PopupType type, bool reuseIfExists = true)
    {
        if (reuseIfExists && _instanceMap.TryGetValue(type, out var existing) && existing != null)
        {
            existing.OnShow();
            _stack.Push(existing);
            return existing;
        }

        if (!_prefabMap.TryGetValue(type, out var prefab) || prefab == null)
        {
            Debug.LogWarning($"PopupManager: No prefab mapped for {type}");
            return null;
        }

        var go = Instantiate(prefab, popupRoot);
        var ui = go.GetComponent<UIBase>();
        if (ui == null)
        {
            Debug.LogWarning("Popup prefab must have a component inheriting UIBase");
            Destroy(go);
            return null;
        }

        _instanceMap[type] = ui;
        // mark as instantiated by manager (not scene-placed)
        _isSceneInstanceMap[type] = false;
        _stack.Push(ui);
        ui.OnShow();
        return ui;
    }

    /// <summary>
    /// Register an existing instance (for scene-placed popups you want to control via manager)
    /// </summary>
    public void RegisterInstance(PopupType type, UIBase instance)
    {
        if (instance == null) return;
        _instanceMap[type] = instance;
        _isSceneInstanceMap[type] = true; // mark this as scene instance
    }

    public void UnregisterInstance(PopupType type, UIBase instance)
    {
        if (instance == null) return;
        if (_instanceMap.TryGetValue(type, out var inst) && inst == instance)
        {
            _instanceMap.Remove(type);
            _isSceneInstanceMap.Remove(type);
        }
    }

    public void CloseTop()
    {
        if (_stack.Count == 0) return;
        var top = _stack.Pop();
        top.OnHide();
        // find associated popup type (if any)
        PopupType? found = null;
        foreach (var kv in _instanceMap)
        {
            if (kv.Value == top) { found = kv.Key; break; }
        }

        if (found.HasValue)
        {
            var type = found.Value;
            var isScene = _isSceneInstanceMap.ContainsKey(type) && _isSceneInstanceMap[type];
            if (!isScene)
                Destroy(top.gameObject, 0.25f);

            _instanceMap.Remove(type);
            _isSceneInstanceMap.Remove(type);
        }
    }

    public void Close(PopupType type)
    {
        if (_instanceMap.TryGetValue(type, out var ui) && ui != null)
        {
            ui.OnHide();
            var isScene = _isSceneInstanceMap.ContainsKey(type) && _isSceneInstanceMap[type];
            if (!isScene)
                Destroy(ui.gameObject, 0.25f);
            _instanceMap.Remove(type);
            _isSceneInstanceMap.Remove(type);

            // remove from stack if present
            var tmp = new Stack<UIBase>();
            while (_stack.Count > 0)
            {
                var x = _stack.Pop();
                if (x != ui) tmp.Push(x);
            }
            while (tmp.Count > 0) _stack.Push(tmp.Pop());
        }
    }

    public void CloseAll()
    {
        while (_stack.Count > 0)
        {
            var ui = _stack.Pop();
            ui.OnHide();
            // find type for this ui to know if it is a scene instance
            PopupType? found = null;
            foreach (var kv in _instanceMap)
            {
                if (kv.Value == ui) { found = kv.Key; break; }
            }
            if (found.HasValue)
            {
                var isScene = _isSceneInstanceMap.ContainsKey(found.Value) && _isSceneInstanceMap[found.Value];
                if (!isScene)
                    Destroy(ui.gameObject, 0.25f);
                _instanceMap.Remove(found.Value);
                _isSceneInstanceMap.Remove(found.Value);
            }
        }
        _instanceMap.Clear();
    }
}

