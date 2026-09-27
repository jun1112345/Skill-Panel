using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class CreateStateScriptUtility
{
    [MenuItem("Assets/Create/Scripting/State", false, 80)]
    public static void CreateState()
    {
        // Caminho do template
        string templatePath = "Assets/Utils/StateMachines/StateScriptTemplate.cs.txt";

        // Verifica se o template existe
        if (!File.Exists(templatePath))
        {
            Debug.LogError($"模板未找到：{templatePath}\n请在 Assets/Editor/Templates/ 文件夹中创建 StateScriptTemplate.cs.txt 文件");
            return;
        }

        // Usa o sistema de criação do Unity que permite renomear
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<DoCreateStateScript>(),
            "NewState.cs",
            EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D,
            templatePath
        );
    }

    internal class DoCreateStateScript : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            string templateContent = File.ReadAllText(resourceFile);

            string className = Path.GetFileNameWithoutExtension(pathName);

            string finalContent = templateContent.Replace("#SCRIPTNAME#", className);

            File.WriteAllText(pathName, finalContent);
            AssetDatabase.Refresh();

            Object newScript = AssetDatabase.LoadAssetAtPath<Object>(pathName);
            ProjectWindowUtil.ShowCreatedAsset(newScript);
        }
    }

}
