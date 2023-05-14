using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Manchy.Rendering.Tiny
{
    [CreateAssetMenu]
    public class TinyRenderPipelineAsset : RenderPipelineAsset
    {
        protected override RenderPipeline CreatePipeline()
        {
            return new TinyRenderPipeline(this);
        }
    }
}