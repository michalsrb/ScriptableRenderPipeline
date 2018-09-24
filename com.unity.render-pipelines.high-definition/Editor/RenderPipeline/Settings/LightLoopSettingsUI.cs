using UnityEditor.AnimatedValues;
using UnityEngine;

namespace UnityEditor.Experimental.Rendering.HDPipeline
{
    using CED = CoreEditorDrawer<LightLoopSettingsUI, SerializedLightLoopSettings>;

    class LightLoopSettingsUI : BaseUI<SerializedLightLoopSettings>
    {
        const string lightLoopSettingsHeaderContent = "Light Loop Settings";
        static readonly GUIContent tileAndClusterContent = CoreEditorUtils.GetContent("Enable Tile And Cluster");
        static readonly GUIContent fptlForForwardOpaqueContent = CoreEditorUtils.GetContent("Enable FPTL For Forward Opaque");
        static readonly GUIContent bigTilePrepassContent = CoreEditorUtils.GetContent("Enable Big Tile Prepass");
        static readonly GUIContent computeLightEvaluationContent = CoreEditorUtils.GetContent("Enable Compute Light Evaluation");
        static readonly GUIContent computeLightVariantsContent = CoreEditorUtils.GetContent("Enable Compute Light Variants");
        static readonly GUIContent computeMaterialVariantsContent = CoreEditorUtils.GetContent("Enable Compute Material Variants");

        public static CED.IDrawer SectionLightLoopSettings(bool withOverride)
        {
            return CED.FoldoutGroup(
                    lightLoopSettingsHeaderContent,
                    (s, p, o) => s.isSectionExpandedLightLoopSettings,
                    FoldoutOption.Indent,
                    CED.LabelWidth(250, CED.Action((s, p, o) => Drawer_SectionLightLoopSettings(s, p, o, withOverride))),
                    CED.space
                );
        }

        public AnimBool isSectionExpandedLightLoopSettings { get { return m_AnimBools[0]; } }
        public AnimBool isSectionExpandedEnableTileAndCluster { get { return m_AnimBools[1]; } }
        public AnimBool isSectionExpandedComputeLightEvaluation { get { return m_AnimBools[2]; } }

        public LightLoopSettingsUI()
            : base(3)
        {
        }

        public override void Update()
        {
            isSectionExpandedEnableTileAndCluster.target = data.enableTileAndCluster.boolValue;
            isSectionExpandedComputeLightEvaluation.target = data.enableComputeLightEvaluation.boolValue;
            base.Update();
        }

        static void Drawer_SectionLightLoopSettings(LightLoopSettingsUI s, SerializedLightLoopSettings p, Editor owner, bool withOverride)
        {
            //RenderPipelineSettings hdrpSettings = (GraphicsSettings.renderPipelineAsset as HDRenderPipelineAsset).renderPipelineSettings;
            OverridableSettingsArea area = new OverridableSettingsArea(6);

            // Uncomment if you re-enable LIGHTLOOP_SINGLE_PASS multi_compile in lit*.shader
            //area.Add(p.enableTileAndCluster, tileAndClusterContent, a => p.overridesTileAndCluster = a, () => p.overridesTileAndCluster);
            //and add indent:1 or indent:2 regarding indentation you want
            
            if (s.isSectionExpandedEnableTileAndCluster.target)
            {
                area.Add(p.enableFptlForForwardOpaque, fptlForForwardOpaqueContent, () => p.overridesFptlForForwardOpaque, a => p.overridesFptlForForwardOpaque = a);
                area.Add(p.enableBigTilePrepass, bigTilePrepassContent, () => p.overridesBigTilePrepass, a => p.overridesBigTilePrepass = a);
                area.Add(p.enableComputeLightEvaluation, computeLightEvaluationContent, () => p.overridesComputeLightEvaluation, a => p.overridesComputeLightEvaluation = a);
                if (s.isSectionExpandedComputeLightEvaluation.target)
                {
                    area.Add(p.enableComputeLightVariants, computeLightVariantsContent, () => p.overridesComputeLightVariants, a => p.overridesComputeLightVariants = a, indent: 1);
                    area.Add(p.enableComputeMaterialVariants, computeMaterialVariantsContent, () => p.overridesComputeMaterialVariants, a => p.overridesComputeMaterialVariants = a, indent: 1);
                }
            }

            area.Draw(withOverride);
        }
    }
}
