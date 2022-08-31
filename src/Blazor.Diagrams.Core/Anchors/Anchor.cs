﻿using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models.Base;
using System;
using System.Collections.Generic;

namespace Blazor.Diagrams.Core.Anchors
{
    public abstract class Anchor
    {
        public Anchor(ILinkable model, Point? offset = null)
        {
            Model = model;
            Offset = offset ?? Point.Zero;
        }

        public ILinkable Model { get; }
        public Point Offset { get; }

        public abstract Point? GetPosition(BaseLinkModel link, Point[] route);

        public abstract Point? GetPlainPosition();

        public Point? GetPosition(BaseLinkModel link) => GetPosition(link, Array.Empty<Point>());

        protected static Point? GetOtherPosition(BaseLinkModel link, bool isTarget)
        {
            if (!isTarget && link.Target == null)
                return link.OnGoingPosition;

            var anchor = isTarget ? link.Source : link.Target!;
            return anchor.GetPlainPosition();
        }

        protected static Point? GetClosestPointTo(IEnumerable<Point?> points, Point point)
        {
            var minDist = double.MaxValue;
            Point? minPoint = null;

            foreach (var pt in points)
            {
                if (pt == null)
                    continue;
                
                var dist = pt.DistanceTo(point);
                if (dist < minDist)
                {
                    minDist = dist;
                    minPoint = pt;
                }
            }

            return minPoint;
        }
    }
}
