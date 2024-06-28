// StoryObjectFactory.cs in VNCreator assembly
namespace VNCreator
{
    using Shared;
    using UnityEngine;
    

    public class StoryObjectFactory : IStoryObjectFactory
    {
        public IStoryObject CreateStoryObject()
        {
            // return null;
            return ScriptableObject.CreateInstance<StoryObject>();
        }
    }
}
