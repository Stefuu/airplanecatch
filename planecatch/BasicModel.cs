using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace planecatch
{
    public class BasicModel
    {
        public Model Model { get; protected set; }
        protected Matrix World = Matrix.Identity;

        public BasicModel(Model model)
        {
            Model = model;
        }

        public virtual void Update()
        {
            
        }
        
        public virtual Matrix GetWorld()
        {
            return World;
        }
        
        public virtual void Draw(Camera camera)
        {
            Draw(camera, null);
        }

        public virtual void Draw(Camera camera, GraphicsDevice device)
        {
            var transforms = new Matrix[Model.Bones.Count];
            Model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (var mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = camera.Projection;
                    effect.View = camera.View;
                    effect.World = GetWorld() * mesh.ParentBone.Transform;
                }

                mesh.Draw();
            }
        }
    }
}
