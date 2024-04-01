using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;
using UnityEngine.UIElements;
using VNCreator.Editors.Graph;

namespace VNCreator
{
    public class SaveUtility
    {
#if UNITY_EDITOR
        public void SaveGraph(StoryObject _story, ExtendedGraphView _graph)
        {
            EditorUtility.SetDirty(_story);

            List<NodeData> nodes = new List<NodeData>();
            List<Link> links = new List<Link>();

            foreach (BaseNode _node in _graph.nodes.ToList().Cast<BaseNode>().ToList())
            {
                nodes.Add(
                new NodeData
                {
                    guid = _node.nodeData.guid,
                    characterSpr = _node.nodeData.characterSpr,
                    characterName = _node.nodeData.characterName,
                    dialogueText = _node.nodeData.dialogueText,
                    backgroundSpr = _node.nodeData.backgroundSpr,
                    startNode = _node.nodeData.startNode,
                    GoodOrBad = _node.nodeData.GoodOrBad,
                    endNode = _node.nodeData.endNode,
                    choices = _node.nodeData.choices,
                    choiceOptions = _node.nodeData.choiceOptions,
                    nodePosition = _node.GetPosition(),
                    soundEffect = _node.nodeData.soundEffect,
                    backgroundMusic = _node.nodeData.backgroundMusic
                });
            }

            List<Edge> _edges = _graph.edges.ToList();
            for (int i = 0; i < _edges.Count; i++)
            {
                BaseNode _output = (BaseNode)_edges[i].output.node;
                BaseNode _input = (BaseNode)_edges[i].input.node;

                links.Add(new Link
                {
                    guid = _output.nodeData.guid,
                    targetGuid = _input.nodeData.guid,
                    portId = i
                });
            }

            _story.SetLists(nodes, links);

            //_story.nodes = nodes;
            //_story.links = links;
        }

        public void LoadGraph(StoryObject _story, ExtendedGraphView _graph)
        {
            foreach (NodeData _data in _story.nodes)
            {
                BaseNode _tempNode = _graph.CreateNode("", _data.nodePosition.position, _data.choices, _data.choiceOptions, _data.startNode, _data.endNode, _data);
                _graph.AddElement(_tempNode);
            }

            GenerateLinks(_story, _graph);
        }

        void GenerateLinks(StoryObject _story, ExtendedGraphView _graph)
        {
            List<BaseNode> _nodes = _graph.nodes.ToList().Cast<BaseNode>().ToList();

            foreach (Link _link in _story.links)
            {
                BaseNode _outputNode = _nodes.FirstOrDefault(x => x.nodeData.guid == _link.guid);
                BaseNode _inputNode = _nodes.FirstOrDefault(x => x.nodeData.guid == _link.targetGuid);

                if (_outputNode == null || _inputNode == null)
                {
                    Debug.LogWarning("Een van de nodes is niet gevonden bij het genereren van links.");
                    continue; // Sla deze link over als een van de nodes niet gevonden is.
                }

                // Verkrijg de juiste output port. Aangenomen wordt dat elke node een passende port heeft voor elke link.
                // Pas op dat je hier niet buiten de grenzen gaat.
                Port _outputPort = null;
                if (_outputNode.outputContainer.childCount > _link.portId)
                {
                    _outputPort = _outputNode.outputContainer[_link.portId].Q<Port>();
                }

                // Controleer of de output port gevonden is.
                if (_outputPort == null)
                {
                    Debug.LogWarning($"Output port niet gevonden voor node {_outputNode.nodeData.guid} op port index {_link.portId}");
                    continue; // Sla deze link over als de output port niet gevonden is.
                }

                // De input port wordt verondersteld de eerste te zijn, wat in de meeste gevallen waarschijnlijk klopt.
                // Pas deze logica aan als je meerdere input ports hebt.
                Port _inputPort = (Port)_inputNode.inputContainer[0];

                // Maak nu de verbinding.
                LinkNodes(_outputPort, _inputPort, _graph);
            }
        }


        void LinkNodes(Port _output, Port _input, ExtendedGraphView _graph)
        {
            //Debug.Log(_output);

            Edge _temp = new Edge
            {
                output = _output,
                input = _input
            };
            Debug.Log(_temp);
            _temp.input.Connect(_temp);
            _temp.output.Connect(_temp);
            _graph.Add(_temp);
        }
#endif
    }
}
