namespace UnityEditor.Experimental.Rendering.HDPipeline
{
    using UnityEngine;
    using UnityEngine.Experimental.Rendering.HDPipeline;
    using UnityEngine.Rendering;
    using CED = CoreEditorDrawer<HDProbeUI, SerializedHDProbe>;

    partial class PlanarReflectionProbeUI : HDProbeUI
    {
        public static readonly CED.IDrawer[] Inspector;

        static PlanarReflectionProbeUI()
        {
            //copy HDProbe UI
            int max = HDProbeUI.Inspector.Length;
            Inspector = new CED.IDrawer[max];
            for(int i = 0; i < max; ++i)
            {
                Inspector[i] = HDProbeUI.Inspector[i];
            }

            //override SectionInfluenceVolume to remove normals settings
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
    }
}
