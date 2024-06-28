using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IStoryObjectFactory.cs in Shared assembly
namespace Shared
{
    public interface IStoryObjectFactory
    {
        IStoryObject CreateStoryObject();
    }
}

