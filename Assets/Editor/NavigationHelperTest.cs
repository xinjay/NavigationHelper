using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
public class NavigationHelperTest : MonoBehaviour
{
    [MenuItem("Test/AgentTest")]
    static void AgentTest()
    {
        var navMeshProjectSettings = NavigationHelper.GetNavMeshProjectSettings();
        var agent0 = navMeshProjectSettings.GetAgentByIndex(0);
        Debug.Log($"Index={agent0.AgentIndex},Name={agent0.AgentName}," +
                  $"Radius={agent0.AgentRadius},Height={ agent0.AgentHeight}," +
                  $"StepHeight={agent0.AgentStepHeight},MaxSlope={ agent0.AgentMaxSlope}");
        agent0.AgentName = "AgentTest";
        agent0.AgentRadius = 1;
        agent0.AgentHeight = 1;
        agent0.AgentStepHeight = 1;
        agent0.AgentMaxSlope = 60;
        Debug.Log($"Index={agent0.AgentIndex},Name={agent0.AgentName}," +
                  $"Radius={agent0.AgentRadius},Height={ agent0.AgentHeight}," +
                  $"StepHeight={agent0.AgentStepHeight},MaxSlope={ agent0.AgentMaxSlope}");
    }

    [MenuItem("Test/AreaTest")]
    static void AreaTest()
    {
        var navMeshProjectSettings = NavigationHelper.GetNavMeshProjectSettings();
        var area = navMeshProjectSettings.GetNavigationArea(3);
        Debug.Log($"Index={area.AreaIndex},name={area.Name},cost={area.Cost}");
        area.Name = "AreaTest";
        area.Cost = 5;
        Debug.Log($"Index={area.AreaIndex},name={area.Name},cost={area.Cost}");
    }
    [MenuItem("Test/BakeTest")]
    static void BakeTest()
    {
        var bakeSetter = NavigationHelper.GetNavMeshSettings(SceneManager.GetActiveScene());
        Debug.Log($"AgentRadius={bakeSetter.AgentRadius},AgentHeight={bakeSetter.AgentHeight}" +
                  $",MaxSlope={bakeSetter.MaxSlope},StepHeight={bakeSetter.StepHeight}" +
                  $",DropHeight={bakeSetter.DropHeight},JumpDistance={bakeSetter.JumpDistance}" +
                  $",ManualVoxelSize={bakeSetter.ManualVoxelSize},VoxelSize={bakeSetter.VoxelSize}" +
                  $",MinRegionArea={bakeSetter.MinRegionArea},HeightMesh={bakeSetter.HeightMesh}");
        bakeSetter.AgentRadius = 1;
        bakeSetter.AgentHeight = 1;
        bakeSetter.MaxSlope = 1;
        bakeSetter.StepHeight = 1;
        bakeSetter.DropHeight = 1;
        bakeSetter.JumpDistance = 1;
        bakeSetter.ManualVoxelSize = true;
        bakeSetter.VoxelSize = 1;
        bakeSetter.MinRegionArea = 1;
        bakeSetter.HeightMesh = true;
        Debug.Log($"AgentRadius={bakeSetter.AgentRadius},AgentHeight={bakeSetter.AgentHeight}" +
                  $",MaxSlope={bakeSetter.MaxSlope},StepHeight={bakeSetter.StepHeight}" +
                  $",DropHeight={bakeSetter.DropHeight},JumpDistance={bakeSetter.JumpDistance}" +
                  $",ManualVoxelSize={bakeSetter.ManualVoxelSize},VoxelSize={bakeSetter.VoxelSize}" +
                  $",MinRegionArea={bakeSetter.MinRegionArea},HeightMesh={bakeSetter.HeightMesh}");
    }

    [MenuItem("Test/Redirect")]
    static void Redirect()
    {
        var src = "Assets/Test1.unity";
        var target = "Assets/Test2.unity";
        var srcScene = EditorSceneManager.OpenScene(src, OpenSceneMode.Single);
        var navidata = NavigationHelper.GetNavMeshData(srcScene);
        var targetScene = EditorSceneManager.OpenScene(target, OpenSceneMode.Single);
        NavigationHelper.SetNavMeshData(targetScene, navidata);
    }
}
