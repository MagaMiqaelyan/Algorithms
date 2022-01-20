using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Geometry
{
    public struct PointBig
    {
        public BigInteger x;
        public BigInteger y;
        public BigInteger angle;
        public int id;
    }

    class Program
    {
        static void Main(string[] args)
        {
            GoatintheGarden2();
        }

        private static double a;
        private static double b;
        private static double n;
        static void GMPineapple()
        {
            var abn = Console.ReadLine().Split().Select(double.Parse).ToArray();
            a = abn[0];
            b = abn[1];
            n = abn[2];
            var step = b / n;

            a /= 2;
            b /= 2;
            var x = b;
            for (int i = 1; i <= n; i++)
            {
                var temp = F(step * i) - F(step * (i - 1));
                Console.WriteLine($"{Math.Abs(temp):0.000000}");
            }
        }

        static double F(double x)
        {
            return Math.PI * a * a * (x * x / b - (x * x * x) / (3 * b * b));
        }

        static void Timus1052()
        {
            var n = int.Parse(Console.ReadLine());
            var points = new List<Point>();
            for (int i = 0; i < n; i++)
            {
                var xy = Console.ReadLine().Split();
                points.Add(new Point(int.Parse(xy[0]), int.Parse(xy[1])));
            }
            Console.WriteLine(MaxPointsOnLine(points));
        }

        static int MaxPointsOnLine(List<Point> points)
        {
            var n = points.Count;
            if (n < 2)
                return n;
            var maxPoint = 0;
            var slopePairs = new Dictionary<Point, int>();
            for (int i = 0; i < n; i++)
            {
                int overlapPoints;
                int verticalPoints;
                var curMax = overlapPoints = verticalPoints = 0;
                for (int j = i + 1; j < n; j++)
                {
                    if (points[i] == points[j])
                        overlapPoints++;
                    else if (points[i].X == points[j].X)
                        verticalPoints++;
                    else
                    {
                        var deltaY = points[j].Y - points[i].Y;
                        var deltaX = points[j].X - points[i].X;
                        var g = GCD(deltaX, deltaY);
                        deltaY /= g;
                        deltaX /= g;
                        var point = new Point(deltaY, deltaX);
                        if (!slopePairs.ContainsKey(point))
                            slopePairs.Add(point, 0);
                        slopePairs[point]++;
                        curMax = Math.Max(curMax, slopePairs[point]);
                    }
                    curMax = Math.Max(curMax, verticalPoints);
                }
                maxPoint = Math.Max(maxPoint, curMax + overlapPoints + 1);
                slopePairs.Clear();
            }
            return maxPoint;
        }

        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        static List<Tuple<long, long, int>> tastes = new List<Tuple<long, long, int>>();
        static void Timus2067()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();
                var s = long.Parse(input[0]);
                var r = long.Parse(input[1]);
                tastes.Add(Tuple.Create(s, r, i + 1));
            }

            if (n == 2)
            {
                Console.WriteLine(1);
                Console.WriteLine($"1 2");
                return;
            }

            var a = true;
            for (int i = 2; i < n; i++)
            {
                if (!IsBestFriend2(i - 2, i - 1, i))
                {
                    a = false;
                    break;
                }
            }

            if (a)
            {
                var l = tastes.OrderBy(x => x.Item1).ThenBy(y => y.Item2);
                Console.WriteLine(1);
                Console.WriteLine($"{l.First().Item3} {l.Last().Item3}");
            }
            else
                Console.WriteLine(0);
        }

        static bool IsBestFriend2(int v, int u, int w)
        {
            return (tastes[u].Item2 - tastes[v].Item2) * (tastes[w].Item1 - tastes[v].Item1) ==
                   (tastes[w].Item2 - tastes[v].Item2) * (tastes[u].Item1 - tastes[v].Item1);
        }

        static void GoatintheGarden2()
        {
            var input = Console.In.ReadToEnd()
                .Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse)
                .ToList();
            var ax = input[0];
            var ay = input[1];
            var bx = input[2];
            var by = input[3];
            var cx = input[4];
            var cy = input[5];
            var l = input[6];

            var ac = Math.Sqrt(Math.Pow(ax - cx, 2) + Math.Pow(ay - cy, 2));
            var bc = Math.Sqrt(Math.Pow(bx - cx, 2) + Math.Pow(by - cy, 2));
            var ab = Math.Sqrt(Math.Pow(bx - ax, 2) + Math.Pow(by - ay, 2));

            var p = (ac + bc + ab) / 2;
            var s = Math.Sqrt(p * (p - ac) * (p - ab) * (p - bc));


            double mindif = 0;
            if (ab == 0)
            {
                mindif = ac;
                Console.WriteLine($"{Math.Max(mindif - l, 0):0.00}");
                Console.WriteLine($"{Math.Max(Math.Max(ac, bc) - l, 0):0.00}");
                return;
            }
            var ch = 2 * s / ab;
            var hb = Math.Sqrt(Math.Pow(Math.Max(bc, ac), 2) - Math.Pow(ch, 2));

            if (hb <= ab)
            {
                mindif = ch;
            }
            else if (hb > ab)
            {
                mindif = Math.Min(ac, bc);
            }

            Console.WriteLine($"{Math.Max(mindif - l, 0):0.00}");
            Console.WriteLine($"{Math.Max(Math.Max(ac, bc) - l, 0):0.00}");

            //var mx = (ax + bx) / 2;
            //var my = (ay + by) / 2;
            //var h = Math.Sqrt(Math.Pow(mx - cx, 2) + Math.Pow(my - cy, 2));

            //double minDif;
            //if (ab == 0)
            //{
            //    minDif = ac;
            //}
            //else if (h <= Math.Max(ac, bc) && h >= Math.Min(ac, bc))
            //{
            //    minDif = Math.Min(ac, bc);
            //}
            //else
            //{
            //    minDif = 2 * s / ab;
            //}

            //Console.WriteLine($"{Math.Max(minDif - l, 0):0.00}");
            //Console.WriteLine($"{Math.Max(Math.Max(ac, bc) - l, 0):0.00}");
        }

        static void CircleofWinter()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var x = new int[n];
            var y = new int[n];
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                x[i] = input[0];
                y[i] = input[1];
            }

            var t = 0.5000000000;
            var r = double.MinValue;
            var x0 = t;
            var y0 = t;
            for (int i = 0; i < n; i++)
            {
                var x1 = x[i] - t;
                var y1 = y[i] - t;
                var sqrt = Math.Sqrt(Math.Pow(x1, 2) + Math.Pow(y1, 2));
                if (sqrt > r)
                {
                    r = sqrt;
                    x0 = t;
                    y0 = t;
                }
            }

            Console.WriteLine("{0} {1} {2}", Math.Round(x0, 10), Math.Round(y0, 10), Math.Round(r, 10));
        }

        static void MedianOnThePlane()
        {
            var n = Convert.ToInt32(Console.ReadLine());
            var points = new PointBig[n];
            var minX = new BigInteger(1000000);
            var minId = 0;
            for (int i = 0; i < n; i++)
            {
                var xy = Console.ReadLine().Split(' ').Select(BigInteger.Parse).ToArray();
                points[i] = new PointBig { x = xy[0], y = xy[1], id = i + 1 };
                if (xy[0] < minX)
                {
                    minX = xy[0];
                    minId = i;
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (points[i].id == minId + 1)
                    points[i].angle = long.MinValue;
                else if (points[i].x == points[minId].x)
                    points[i].angle = (points[i].y > points[minId].y) ? 90 : -90;
                else
                    points[i].angle = BigInteger.Divide(BigInteger.Subtract(points[i].y, points[minId].y),
                        BigInteger.Subtract(points[i].x, points[minId].x));
            }

            Console.Write(points[minId].id + " ");
            points = points.OrderBy(p1 => p1.angle).ToArray();
            Console.Write(points[n / 2].id);
        }

        static void AngryBirds()
        {
            var points = new List<KeyValuePair<BigInteger, BigInteger>>();
            for (int i = 0; i < 5; i++)
            {
                var xy = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                points.Add(new KeyValuePair<BigInteger, BigInteger>(new BigInteger(xy[0]), new BigInteger(xy[1])));
            }

            Console.WriteLine(Parabol(points));
        }

        static int Parabol(List<KeyValuePair<BigInteger, BigInteger>> points)
        {
            if (points.Count == 0)
                return 0;
            int ans = points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    var x1 = points[i].Key;
                    var y1 = points[i].Value;
                    var x2 = points[j].Key;
                    var y2 = points[j].Value;
                    var temp = new List<KeyValuePair<BigInteger, BigInteger>>();
                    temp.AddRange(points);
                    var l = 0;
                    while (l < temp.Count && temp.Count > 0)
                    {
                        var x = temp[l].Key;
                        var y = temp[l].Value;

                        if (BigInteger.Multiply(y,
                                BigInteger.Subtract(BigInteger.Multiply(BigInteger.Multiply(x1, x1), x2),
                                    BigInteger.Multiply(BigInteger.Multiply(x2, x2), x1)))
                            == BigInteger.Add(
                                BigInteger.Multiply(BigInteger.Multiply(x, x),
                                    BigInteger.Subtract(BigInteger.Multiply(x2, y1), BigInteger.Multiply(x1, y2))),
                                BigInteger.Multiply(x, BigInteger.Subtract(
                                    BigInteger.Multiply(BigInteger.Multiply(x1, x1), y2),
                                    BigInteger.Multiply(BigInteger.Multiply(x2, x2), y1))))
                            && CompareAandB(x2 * y1 - y2 * x1,
                                BigInteger.Subtract(BigInteger.Multiply(BigInteger.Multiply(x1, x1), x2),
                                    BigInteger.Multiply(BigInteger.Multiply(x2, x2), x1))))
                            temp.Remove(temp[l]);
                        else
                            l++;
                    }

                    if (temp.Count == points.Count)
                        continue;
                    ans = Math.Min(ans, 1 + Parabol(temp));
                }
            }

            return ans;
        }

        static bool CompareAandB(BigInteger a, BigInteger b)
        {
            return a > 0 ? b < 0 : b > 0;
        }

        static void Yekaterinozavodsk()
        {
            var well = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var type = well[0];
            var size = well[1];
            var ans = 0;
            double maxLength = MaxLength(type, size);
            var n = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var temp = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
                if (MinLength(temp[0], temp[1]) <= maxLength)
                    ans++;
            }

            Console.WriteLine(ans);
        }

        private static double MaxLength(int type, int size)
        {
            if (type == 1) return 2 * size;
            if (type == 2) return Math.Sqrt(2) * size;
            return size;
        }

        private static double MinLength(int type, int size)
        {
            if (type == 1) return 2 * size;
            if (type == 2) return size;
            return Math.Sqrt(3) / 2 * size;
        }

        private static void BallInDream()
        {
            var input = Console.ReadLine().Split(' ').Select(Double.Parse).ToArray();
            var v = input[0];
            var a = input[1];
            var k = input[2];

            var pi = 3.1415926535;
            var vx = Math.Cos((Math.PI / 180) * a) * v;
            var vy = Math.Sin((Math.PI / 180) * a) * v;
            var t = 2 * vy / 10;
            var s0 = t * vx;
            Console.WriteLine(String.Format("{0:0.00}", s0 / (1 - (1 / k))));
        }

        private static void Rope()
        {
            var input = Console.ReadLine().Split(' ');
            double r = Convert.ToDouble(input[1]);
            int n = Convert.ToInt32(input[0]);
            double[] x = new double[n];
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                var currentXY = Console.ReadLine().Split(' ');
                x[i] = Convert.ToDouble(currentXY[0]);
                y[i] = Convert.ToDouble(currentXY[1]);
            }

            var s = 2 * Math.PI * r +
                    Math.Sqrt((x[n - 1] - x[0]) * (x[n - 1] - x[0]) + (y[n - 1] - y[0]) * (y[n - 1] - y[0]));
            for (int i = 1; i < n; i++)
            {
                var cx = x[i] - x[i - 1];
                var cy = y[i] - y[i - 1];

                s += Math.Sqrt(cx * cx + cy * cy);
            }

            Console.WriteLine(String.Format("{0:0.00}", s));
        }

        static void GoatinTheGarden()
        {
            var input = Console.ReadLine()
                ?.Split(' ')
                .Select(double.Parse).ToArray();
            double l = input[0];
            double r = input[1];

            if (2 * r <= l)
                Console.WriteLine($"{Math.PI * r * r:0.000}");
            else if (r >= l * Math.Sqrt(2) / 2)
                Console.WriteLine($"{l * l:0.000}");
            else
                Console.WriteLine(
                    $"{l * Math.Sqrt(4 * r * r - l * l) + (Math.PI - 4 * Math.Acos(l / 2 / r)) * r * r:0.000}");
        }
    }
}
