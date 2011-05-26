using Microsoft.Xna.Framework;

namespace planecatch
{
    public class Collision : GameComponent
    {
        private Vector3 _lastPosition;

        private PlaneCatchGame PlaneCatchGame { get { return (PlaneCatchGame)Game; } }


        public Collision(Game game) : base(game)
        {
            
        }

        public override void Initialize()
        {
            _lastPosition = PlaneCatchGame.Camera.Position;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Todo: alterar para usar os services
            var player = PlaneCatchGame.Camera;

            var modelManager = (IModelManager)Game.Services.GetService(typeof(IModelManager));
            
            ModelCollision(modelManager, player);

            _lastPosition = PlaneCatchGame.Camera.Position;

            base.Update(gameTime);
        }

        private void ModelCollision(IModelManager modelManager, Camera player)
        {
            foreach (BasicModel basicModel in modelManager.Models)
            {
                if (basicModel.ToString() == "planecatch.Skybox")
                    continue;

                var boundingBox = (BoundingBox)basicModel.Model.Tag;

                if (boundingBox.Contains(player.BoundingShpere) == ContainmentType.Disjoint) continue;
                
                player.Position = _lastPosition;
            }
        }
    }
}