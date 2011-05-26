using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Extensions
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "Extensions.PerMeshBoundingBoxContentProcessor")]
    public class PerMeshBoundingBoxContentProcessor : ModelProcessor
    {
        float _minX = float.MaxValue;
        float _minY = float.MaxValue;
        float _minZ = float.MaxValue;
        float _maxX = float.MinValue;
        float _maxY = float.MinValue;
        float _maxZ = float.MinValue;

        public override ModelContent Process(NodeContent input, ContentProcessorContext context)
        {
            NodeContentCollection nodeContentCollection = input.Children;

            //This is a recursive function in case the input's children have children.
            ParseChildren(nodeContentCollection);

            var modelContent = base.Process(input, context);

            var min = new Vector3(_minX, _minY, _minZ);
            var max = new Vector3(_maxX, _maxY, _maxZ);

            modelContent.Tag = new BoundingBox(min, max);

            return modelContent;
        }

        private void ParseChildren(IEnumerable<NodeContent> nodeContentCollection)
        {
            foreach (var nodeContent in nodeContentCollection)
            {
                if (nodeContent is MeshContent)
                {
                    var meshContent = (MeshContent)nodeContent;
                    foreach (var vector in meshContent.Positions)
                    {
                        if (vector.X < _minX)
                            _minX = vector.X;

                        if (vector.Y < _minY)
                            _minY = vector.Y;

                        if (vector.Z < _minZ)
                            _minZ = vector.Z;

                        if (vector.X > _maxX)
                            _maxX = vector.X;

                        if (vector.Y > _maxY)
                            _maxY = vector.Y;

                        if (vector.Z > _maxZ)
                            _maxZ = vector.Z;
                    }
                }
                else
                {
                    ParseChildren(nodeContent.Children);
                }
            }
        }
    }
}