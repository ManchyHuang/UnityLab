using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Manchy.Rendering.Tiny
{
    public class TinyRenderPipeline : RenderPipeline
    {
        TinyRenderPipelineAsset _asset;
        public TinyRenderPipeline(TinyRenderPipelineAsset asset)
        {
            _asset = asset;
        }

        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (var camera in cameras)
            {
                //0.设置相机相关的全局Shader变量
                context.SetupCameraProperties(camera);

                //1.渲染天空盒
                context.DrawSkybox(camera);


                //2.相机裁剪
                camera.TryGetCullingParameters(out var cullingParameters);


                //3.渲染非透明物体
                {
                    var cullingResults = context.Cull(ref cullingParameters);
                    var drawingSettings = new DrawingSettings();
                    drawingSettings.SetShaderPassName(1, new ShaderTagId("SRPDefaultUnlit"));
                    drawingSettings.sortingSettings = new SortingSettings() { criteria = SortingCriteria.CommonOpaque };

                    var filteringSettings = new FilteringSettings(RenderQueueRange.opaque, -1, uint.MaxValue);
                    context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
                }

                //4.渲染透明物体
                {
                    var cullingResults = context.Cull(ref cullingParameters);
                    var drawingSettings = new DrawingSettings();
                    drawingSettings.SetShaderPassName(1, new ShaderTagId("SRPDefaultUnlit"));
                    drawingSettings.sortingSettings = new SortingSettings() { criteria = SortingCriteria.CommonTransparent };

                    var filteringSettings = new FilteringSettings(RenderQueueRange.transparent, -1, uint.MaxValue);
                    context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
                }

                //5.后处理

            }

            //6.提交
            context.Submit();
        }
    }
}