using UnityEngine;

namespace GameEngine
{
    public sealed class SwitchSkinnedMeshRendererMechanics
    {
        private readonly SkinnedMeshRenderer _meshRenderer;

        public SwitchSkinnedMeshRendererMechanics(SkinnedMeshRenderer meshRenderer)
        {
            _meshRenderer = meshRenderer;
        }
        
        public void OnEnable()
        {
            if (_meshRenderer.enabled) return;
            
            _meshRenderer.enabled = true;
        }
    }
}