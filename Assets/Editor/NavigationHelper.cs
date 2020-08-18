using UnityEngine;
using UnityEditor;
using UnityEditor.AI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class NavigationHelper
{
    public static NavMeshProjectSettingsParamSetter GetNavMeshProjectSettings()
    {
        return new NavMeshProjectSettingsParamSetter();
    }
    public static NavMeshSettingsBakeParamSetter GetNavMeshSettings(Scene scene)
    {
        return new NavMeshSettingsBakeParamSetter(scene);
    }
    public static Object GetNavMeshData(Scene scene)
    {
        SceneManager.SetActiveScene(scene);
        var naviMeshSettings = new SerializedObject(NavMeshBuilder.navMeshSettingsObject);
        var prop = naviMeshSettings.FindProperty("m_NavMeshData");
        return prop != null ? prop.objectReferenceValue : null;
    }
    public static void SetNavMeshData(Scene scene, Object obj)
    {
        SceneManager.SetActiveScene(scene);
        var naviMeshSettings = new SerializedObject(NavMeshBuilder.navMeshSettingsObject);
        var prop = naviMeshSettings.FindProperty("m_NavMeshData");
        if (prop != null)
        {
            prop.objectReferenceValue = obj;
            naviMeshSettings.ApplyModifiedProperties();
        }
        EditorSceneManager.SaveScene(scene, scene.path);
    }

}
public class NavMeshProjectSettingsParamSetter
{
    private SerializedObject m_navMeshProjectSettingsSo;
    internal NavMeshProjectSettingsParamSetter()
    {
        var serializedAssetInterfaceSingleton = Unsupported.GetSerializedAssetInterfaceSingleton("NavMeshProjectSettings");
        m_navMeshProjectSettingsSo = new SerializedObject(serializedAssetInterfaceSingleton);
    }
    public NavigationAgent GetAgentByIndex(int index = 0)
    {
        return new NavigationAgent(m_navMeshProjectSettingsSo, index);
    }
    public int GetAgentsCount()
    {
        var agents = m_navMeshProjectSettingsSo.FindProperty("m_Settings");
        return agents.arraySize;
    }
    public string[] GetAgentNames()
    {
        var agentnamesProp = m_navMeshProjectSettingsSo.FindProperty("m_SettingNames");
        var names = new string[agentnamesProp.arraySize];
        for (var index = 0; index < names.Length; index++)
        {
            names[index] = agentnamesProp.GetArrayElementAtIndex(index).stringValue;
        }
        return names;
    }
    public NavigationArea GetNavigationArea(int index = 0)
    {
        return new NavigationArea(m_navMeshProjectSettingsSo, index);
    }
}
public class NavMeshSettingsBakeParamSetter
{
    private SerializedObject m_navigationSettings;
    private SerializedProperty m_agentRadius;
    private SerializedProperty m_agentHeight;
    private SerializedProperty m_maxSlope;
    private SerializedProperty m_stepHeight;
    private SerializedProperty m_dropHeight;
    private SerializedProperty m_jumpDistance;
    private SerializedProperty m_manualVoxelSize;
    private SerializedProperty m_voxelSize;
    private SerializedProperty m_minRegionArea;
    private SerializedProperty m_heightMesh;
    internal NavMeshSettingsBakeParamSetter(Scene scene)
    {
        SceneManager.SetActiveScene(scene);
        m_navigationSettings = new SerializedObject(NavMeshBuilder.navMeshSettingsObject);
        m_agentRadius = m_navigationSettings.FindProperty("m_BuildSettings.agentRadius");
        m_agentHeight = m_navigationSettings.FindProperty("m_BuildSettings.agentHeight");
        m_maxSlope = m_navigationSettings.FindProperty("m_BuildSettings.agentSlope");
        m_stepHeight = m_navigationSettings.FindProperty("m_BuildSettings.agentClimb");
        m_dropHeight = m_navigationSettings.FindProperty("m_BuildSettings.ledgeDropHeight");
        m_jumpDistance = m_navigationSettings.FindProperty("m_BuildSettings.maxJumpAcrossDistance");
        m_manualVoxelSize = m_navigationSettings.FindProperty("m_BuildSettings.manualCellSize");
        m_voxelSize = m_navigationSettings.FindProperty("m_BuildSettings.cellSize");
        m_minRegionArea = m_navigationSettings.FindProperty("m_BuildSettings.minRegionArea");
        m_heightMesh = m_navigationSettings.FindProperty("m_BuildSettings.accuratePlacement");
    }
    public float AgentRadius
    {
        get { return m_agentRadius.floatValue; }
        set
        {
            m_agentRadius.floatValue = value;
            m_navigationSettings.ApplyModifiedProperties();
        }
    }
    public float AgentHeight
    {
        get { return m_agentHeight.floatValue; }
        set
        {
            m_agentHeight.floatValue = value;
            m_navigationSettings.ApplyModifiedProperties();
        }
    }
    public float MaxSlope
    {
        get { return m_maxSlope.floatValue; }
        set
        {
            m_maxSlope.floatValue = value;
            m_navigationSettings.ApplyModifiedProperties();
        }
    }
    public float StepHeight
    {
        get { return m_stepHeight.floatValue; }
        set
        {
            m_stepHeight.floatValue = value;
            m_navigationSettings.ApplyModifiedProperties();
        }
    }
    public float DropHeight
    {
        get { return m_dropHeight.floatValue; }
        set
        {
            m_dropHeight.floatValue = value;
            m_navigationSettings.ApplyModifiedProperties();
        }
    }
    public float JumpDistance
    {
        get { return m_jumpDistance.floatValue; }
        set
        {
            m_jumpDistance.floatValue = value;
            m_navigationSettings.ApplyModifiedProperties();
        }
    }
    public bool ManualVoxelSize
    {
        get { return m_manualVoxelSize.boolValue; }
        set
        {
            m_manualVoxelSize.boolValue = value;
            m_navigationSettings.ApplyModifiedProperties();
        }
    }
    public float VoxelSize
    {
        get { return m_voxelSize.floatValue; }
        set
        {
            m_voxelSize.floatValue = value;
            m_navigationSettings.ApplyModifiedProperties();
        }
    }
    public float MinRegionArea
    {
        get { return m_minRegionArea.floatValue; }
        set
        {
            m_minRegionArea.floatValue = value;
            m_navigationSettings.ApplyModifiedProperties();
        }
    }
    public bool HeightMesh
    {
        get { return m_heightMesh.boolValue; }
        set
        {
            m_heightMesh.boolValue = value;
            m_navigationSettings.ApplyModifiedProperties();
        }
    }
}


public class NavigationAgent
{
    private SerializedObject m_navMeshProjectSettingsObject;
    private int agentIndex;
    private SerializedProperty m_agentName;
    private SerializedProperty m_agentRadius;
    private SerializedProperty m_agentHeight;
    private SerializedProperty m_agentStepHeight;
    private SerializedProperty m_agentMaxSlope;
    public NavigationAgent(SerializedObject navMeshProjectSettingsObject, int agentIndex)
    {
        this.m_navMeshProjectSettingsObject = navMeshProjectSettingsObject;
        this.agentIndex = agentIndex;
        var _agents = m_navMeshProjectSettingsObject.FindProperty("m_Settings");
        var _settingNames = m_navMeshProjectSettingsObject.FindProperty("m_SettingNames");
        var agent = _agents.GetArrayElementAtIndex(agentIndex);
        m_agentName = _settingNames.GetArrayElementAtIndex(agentIndex);
        m_agentRadius = agent.FindPropertyRelative("agentRadius");
        m_agentHeight = agent.FindPropertyRelative("agentHeight");
        m_agentStepHeight = agent.FindPropertyRelative("agentClimb");
        m_agentMaxSlope = agent.FindPropertyRelative("agentSlope");
    }

    public int AgentIndex
    {
        get { return agentIndex; }
    }
    public string AgentName
    {
        get { return m_agentName.stringValue; }
        set
        {
            m_agentName.stringValue = value;
            m_navMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }
    public float AgentRadius
    {
        get { return m_agentRadius.floatValue; }
        set
        {
            m_agentRadius.floatValue = value;
            m_navMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }
    public float AgentHeight
    {
        get { return m_agentHeight.floatValue; }
        set
        {
            m_agentHeight.floatValue = value;
            m_navMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }
    public float AgentStepHeight
    {
        get { return m_agentStepHeight.floatValue; }
        set
        {
            m_agentStepHeight.floatValue = value;
            m_navMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }
    public float AgentMaxSlope
    {
        get { return m_agentMaxSlope.floatValue; }
        set
        {
            m_agentMaxSlope.floatValue = value;
            m_navMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }
}
public class NavigationArea
{
    private SerializedObject m_navMeshProjectSettingsObject;
    private int areaIndex;
    private SerializedProperty m_name;
    private SerializedProperty m_cost;
    internal NavigationArea(SerializedObject navMeshProjectSettingsObject, int areaIndex)
    {
        this.m_navMeshProjectSettingsObject = navMeshProjectSettingsObject;
        this.areaIndex = areaIndex;
        var areas = m_navMeshProjectSettingsObject.FindProperty("areas");
        var size = areas.arraySize;
        if (areaIndex < 0 || areaIndex > areas.arraySize - 1)
        {
            throw new System.ArgumentOutOfRangeException("Ô½½ç");
        }
        var area = areas.GetArrayElementAtIndex(areaIndex);
        m_name = area.FindPropertyRelative("name");
        m_cost = area.FindPropertyRelative("cost");
    }
    public int AreaIndex
    {
        get { return areaIndex; }
    }
    public string Name
    {
        get { return m_name.stringValue; }
        set
        {
            m_name.stringValue = value;
            m_navMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }
    public float Cost
    {
        get { return m_cost.floatValue; }
        set
        {
            m_cost.floatValue = value;
            m_navMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }
}
