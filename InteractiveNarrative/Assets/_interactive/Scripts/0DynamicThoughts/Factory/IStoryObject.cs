// IStoryObject.cs in Shared assembly
using System.Collections.Generic;

namespace Shared
{
    public interface IStoryObject
    {
        List<Link> Links { get; set; }
        List<NodeData> Nodes { get; set; }
        string Context { get; set; }

        void SetLists(List<NodeData> nodes, List<Link> links);
        NodeData GetFirstNode();
        NodeData GetCurrentNode(string currentGuid);
        NodeData GetNextNode(string currentGuid, int choiceId);
    }
}

