namespace UnityEditor.Experimental.Rendering.HDPipeline
{
    using UnityEngine;
    using UnityEngine.Experimental.Rendering.HDPipeline;
    using UnityEngine.Rendering;
    using CED = CoreEditorDrawer<HDProbeUI, SerializedHDProbe>;

    partial class PlanarReflectionProbeUI : HDProbeUI
    {
        //static readonly GUIContent overrideFieldOfViewContent = CoreEditorUtils.GetContent("Override Field Of View");
        //static readonly GUIContent fieldOfViewSolidAngleContent = CoreEditorUtils.GetContent("Field Of View");

        public static readonly CED.IDrawer[] Inspector;

        //public static readonly CED.IDrawer SectionFoldoutCaptureSettings = CED.FoldoutGroup(
        //    "Capture Settings",
        //    (s, d, o) => s.isSectionExpandedCaptureSettings,
        //    FoldoutOption.Indent,
        //    CED.Action(Drawer_SectionCaptureSettings)
        //    );

        static PlanarReflectionProbeUI()
        {
            //copy HDProbe UI
            int max = HDProbeUI.Inspector.Length;
            Inspector = new CED.IDrawer[max];
            for(int i = 0; i < max; ++i)
            {
                Inspector[i] = HDProbeUI.Inspector[i];
            }

            //override SectionPrimarySettings
            //Inspector[1] = CED.noop;

            //override SectionInfluenceVolume
            Inspector[3] = CED.Select(
                (s, d, o) => s.influenceVolume,
                (s, d, o) => d.influenceVolume,
                InfluenceVolumeUI.SectionFoldoutShapePlanar
                );
        }

        internal PlanarReflectionProbeUI()
        {
            toolBars = new[] { ToolBar.InfluenceShape | ToolBar.Blend };
        }

        //protected static void Drawer_SectionCaptureSettings(HDProbeUI s, SerializedHDProbe d, Editor o)
        //{
        //    SerializedPlanarReflectionProbe serialized = (SerializedPlanarReflectionProbe)d;
        //    var hdrp = GraphicsSettings.renderPipelineAsset as HDRenderPipelineAsset;
        //    GUI.enabled = false;
        //    EditorGUILayout.LabelField(
        //        CoreEditorUtils.GetContent("Probe Texture Size (Set By HDRP)"),
        //        CoreEditorUtils.GetContent(hdrp.renderPipelineSettings.lightLoopSettings.planarReflectionTextureSize.ToString()),
        //        EditorStyles.label);
        //    EditorGUILayout.Toggle(
        //        CoreEditorUtils.GetContent("Probe Compression (Set By HDRP)"),
        //        hdrp.renderPipelineSettings.lightLoopSettings.planarReflectionCacheCompressed);
        //    GUI.enabled = true;

        //    bool on = serialized.overrideFieldOfView.boolValue;
        //    EditorGUI.BeginChangeCheck();
        //    on = EditorGUILayout.Toggle(overrideFieldOfViewContent, on);
        //    if (on)
        //    {
        //        serialized.fieldOfViewOverride.floatValue = EditorGUILayout.FloatField(fieldOfViewSolidAngleContent, serialized.fieldOfViewOverride.floatValue);
        //    }
        //    if (EditorGUI.EndChangeCheck())
        //    {
        //        serialized.overrideFieldOfView.boolValue = on;
        //        serialized.Apply();
        //    }
        //}

    }
}
