using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEditor.VFX.Block;

namespace UnityEditor.VFX
{
    [VFXInfo]
    class VFXBasicUpdate : VFXContext
    {
        public enum VFXIntegrationMode
        {
            Euler,
            None
        }

        [Header("Particle Update Options")]
        [VFXSetting(VFXSettingAttribute.VisibleFlags.InInspector)]
        public VFXIntegrationMode integration = VFXIntegrationMode.Euler;

        [VFXSetting(VFXSettingAttribute.VisibleFlags.InInspector), Tooltip("Automatically increase particle age every frame, based on deltaTime")]
        public bool ageParticles = true;

        [VFXSetting(VFXSettingAttribute.VisibleFlags.InInspector), Tooltip("Destroy particles if age > lifetime")]
        public bool reapParticles = true;

        public VFXBasicUpdate() : base(VFXContextType.kUpdate, VFXDataType.kParticle, VFXDataType.kParticle) {}
        public override string name { get { return "Update"; } }
        public override string codeGeneratorTemplate { get { return "VFXEditor/Shaders/VFXUpdate"; } }
        public override bool codeGeneratorCompute { get { return true; } }
        public override VFXTaskType taskType { get { return VFXTaskType.Update; } }

        public override IEnumerable<VFXAttributeInfo> attributes
        {
            get
            {
                if (GetData().IsCurrentAttributeRead(VFXAttribute.OldPosition))
                {
                    yield return new VFXAttributeInfo(VFXAttribute.Position, VFXAttributeMode.Read);
                    yield return new VFXAttributeInfo(VFXAttribute.OldPosition, VFXAttributeMode.Write);
                }
            }
        }

        protected override IEnumerable<VFXBlock> implicitPostBlock
        {
            get
            {
                var data = GetData();

                if (integration != VFXIntegrationMode.None && data.IsCurrentAttributeWritten(VFXAttribute.Velocity))
                    yield return CreateInstance<EulerIntegration>();

                var lifeTime = GetData().IsCurrentAttributeWritten(VFXAttribute.Lifetime);
                var age = GetData().IsCurrentAttributeRead(VFXAttribute.Age);

                if (age || lifeTime)
                {
                    if (ageParticles)
                        yield return CreateInstance<Age>();

                    if (lifeTime && reapParticles)
                        yield return CreateInstance<Reap>();
                }
            }
        }

        public override IEnumerable<string> additionalDefines
        {
            get
            {
                if ((GetData() as VFXDataParticle).NeedsIndirectBuffer())
                    yield return "VFX_HAS_INDIRECT_DRAW";
            }
        }
    }
}
