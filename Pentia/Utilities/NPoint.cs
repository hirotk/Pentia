using System;

namespace Pentia.Utilities {
    public struct NPoint {
        public int x, y;

        public NPoint(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static NPoint operator+(NPoint point1, NPoint point2) {
            return new NPoint(point1.x + point2.x, point1.y + point2.y);
        }

        public static NPoint operator-(NPoint point1, NPoint point2) {
            return new NPoint(point1.x - point2.x, point1.y - point2.y);
        }

        public static int operator*(NPoint point1, NPoint point2) {
            return point1.x * point2.x + point1.y * point2.y;           
        }

        public static bool operator==(NPoint point1, NPoint point2) {
            return (point1.x == point2.x && point1.y == point2.y);
        }

        public static bool operator!=(NPoint point1, NPoint point2) {
            return (point1.x != point2.x || point1.y != point2.y);
        }
    }

    public enum Direction {
        Left, Right, Down
    }

    public interface IMovable {
        bool Move(Direction dct);
    }

    public static class Mover {
        public static NPoint Move(NPoint point, Direction direction) {
            switch (direction) {
                case Direction.Left:
                    point.x -= 1;
                    break;
                case Direction.Right:
                    point.x += 1;
                    break;
                case Direction.Down:
                    point.y += 1;
                    break;
            }
            return point;
        }        

        public static void Move(ref NPoint point, Direction direction) {
            point = Move(point, direction);
        }

        public static void Move(NPoint[] points, Direction direction) {
            for (int i = 0; i < points.Length; i++) {
                Move(ref points[i], direction);
            }
        }
    }

    public enum RtDirection {
        Clockwise, CtrClockwise
    }

    public interface IRotatable {
        bool Rotate(RtDirection rdct);
    }

    public static class Rotator {
        public static NPoint Rotate(NPoint point, RtDirection direction) {  // A clockwise rotation matrix in the screen coordinate system
            int a01 = (direction == RtDirection.Clockwise) ? -1 : 1; // (a00, a01) = (cos(pi/2), -sin(pi/2))
            int a10 = (direction == RtDirection.Clockwise) ? 1 : -1; // (a10, a11) = (sin(pi/2),   cos(pi/2))

            int tx = point.x;
            point.x = a01 * point.y;
            point.y = a10 * tx;
            return point;
        }

        public static void Rotate(ref NPoint point, RtDirection direction) {
            point = Rotate(point, direction);
        }

        public static void Rotate(NPoint[] points, RtDirection direction) {
            for (int i = 0; i < points.Length; i++) {
                Rotate(ref points[i], direction);
            }
        }
    }
}
