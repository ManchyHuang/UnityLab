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
                //0.���������ص�ȫ��Shader����
                context.SetupCameraProperties(camera);

                //1.��Ⱦ��պ�
                context.DrawSkybox(camera);


                //2.����ü�
                camera.TryGetCullingParameters(out var cullingParameters);


                //3.��Ⱦ��͸������
                {
                    var cullingResults = context.Cull(ref cullingParameters);
                    var drawingSettings = new DrawingSettings();
                    drawingSettings.SetShaderPassName(1, new ShaderTagId("SRPDefaultUnlit"));
                    drawingSettings.sortingSettings = new SortingSettings() { criteria = SortingCriteria.CommonOpaque };

                    var filteringSettings = new FilteringSettings(RenderQueueRange.opaque, -1, uint.MaxValue);
                    context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
                }

                //4.��Ⱦ͸������
                {
                    var cullingResults = context.Cull(ref cullingParameters);
                    var drawingSettings = new DrawingSettings();
                    drawingSettings.SetShaderPassName(1, new ShaderTagId("SRPDefaultUnlit"));
                    drawingSettings.sortingSettings = new SortingSettings() { criteria = SortingCriteria.CommonTransparent };

                    var filteringSettings = new FilteringSettings(RenderQueueRange.transparent, -1, uint.MaxValue);
                    context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
                }

                //5.����

            }

            //6.�ύ
            context.Submit();
        }
    }
}