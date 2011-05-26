using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace planecatch
{
    public class Skybox : BasicModel
    {
        private Matrix[] skyboxTransforms;

        public Skybox(Model model) : base(model)
        {
            skyboxTransforms = new Matrix[model.Bones.Count];
        }

        public override void Draw(Camera camera, GraphicsDevice device)
        {
             //var playerPos = camera.Position + new Vector3(0, -camera.Position.Y - 10, 0);
            var playerPos = Vector3.Zero - new Vector3(0,2,0);

            Model.CopyAbsoluteBoneTransformsTo(skyboxTransforms);
            
            device.RenderState.DepthBufferWriteEnable = false;
            Model.CopyAbsoluteBoneTransformsTo(skyboxTransforms);
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = skyboxTransforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(playerPos);
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                }

                mesh.Draw();
            }

            device.RenderState.DepthBufferWriteEnable = true;
        }
        
    }
}