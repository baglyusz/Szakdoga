using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Edge = UnityEditor.Experimental.GraphView.Edge;


public class GraphSaveUtility
{

    private DialogueGraphView _targetGraphView;
    private DialogueContainer _containerCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();

    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }
    public void SaveGraph(string fileName)
    {
        if (!Edges.Any()) return; //return if there are no connections

        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray(); // save output choice nodes

        foreach (var t in connectedPorts)
        {
            var outputNode = t.output.node as DialogueNode;
            var inputNode = t.input.node as DialogueNode;

            dialogueContainer.NodeLinks.Add(new NodeLinkData
            {
                baseNodeGuid = outputNode.Guid,
                portName = t.output.portName,
                targetNodeGuid = inputNode.Guid
            });
        }

        foreach (var dialogueNode in Nodes.Where(node => !node.EntryPoint))
        {
            dialogueContainer.DialogueNodeDatas.Add(new DialogueNodeData
            {
                guid = dialogueNode.Guid,
                dialogueText = dialogueNode.DialogueText,
                position = dialogueNode.GetPosition().position
            });
        }

        if (!AssetDatabase.IsValidFolder("Assets/Dialogue/Resources"))
            AssetDatabase.CreateFolder("Dialogue", "Resources");

        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Dialogue/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }


    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DialogueContainer>(fileName);
        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("File not found.", "Target dialogue graph file does not exist.", "OK");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ConnectNodes()
    {
        throw new System.NotImplementedException();
    }

    private void CreateNodes()
    {
        foreach (var nodeData in _containerCache.DialogueNodeDatas)
        {
            var tempNode = _targetGraphView.CreateDialogueNode(nodeData.dialogueText);
            //change guid to saved note's guid
            tempNode.Guid = nodeData.guid;
            _targetGraphView.AddElement(tempNode);

            var nodePorts = _containerCache.NodeLinks.Where(x => x.baseNodeGuid == nodeData.guid).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.portName));
        }
    }

    private void ClearGraph()
    {
        //Set entry point's guid back from the save. Discard existing guid;
        Nodes.Find(x => x.EntryPoint).Guid = _containerCache.NodeLinks[0].baseNodeGuid;

        foreach (var node in Nodes)
        {
            if (node.EntryPoint) return;

            //Remove edges connected to this note
            Edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge => _targetGraphView.RemoveElement(edge)); //remove edges

            //Then remove the node itself;
            _targetGraphView.RemoveElement(node); //remove node
        }
    }
}