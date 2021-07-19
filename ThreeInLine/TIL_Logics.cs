using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeInLine.Models;

namespace ThreeInLine
{
    public class TIL_Logics : Matrix
    {
        private readonly Random _random;
        private readonly int _elements;
        private readonly List<Point> _hor;
        private readonly List<Point> _vert;

        public delegate void DelegateDestruction(int destructionElement);
        public delegate void BonusDestruction(int x, int y, int bonusLevel, int element);

        public event DelegateDestruction OnDestruction;
        public event BonusDestruction OnBonusDestruction;

        public TIL_Logics(int columns, int rows, int elements) : base(columns, rows)
        {
            _elements = elements;
            if (_elements < 3 || _elements > 9)
                throw new ArgumentException("Elements range 3-9");

            _random = new Random();
            _hor = new();
            _vert = new();
            for (int i = 0; i < Rows; i++)
            {
                for (int k = 0; k < Columns; k++)
                {
                    GenerateElement(k, i);
                }
            }

        }

        private void GenerateElement(int x, int y, bool notDestrLine = true)
        {
            bool generation = false;
            do
            {
                this[x, y] = (_random.Next(_elements) + 1) * 10;
                generation = CanMove(x, y);
                if (!notDestrLine)
                    generation = false;
            } while (generation);
        }

        public void Destruction(int x, int y)
        {
            _hor.AddRange(HorVerification(x, y));
            _vert.AddRange(VertVerification(x, y));

            if (_hor.Count > 2)
            {
                foreach (var i in _hor)
                {
                    _vert.AddRange(VertVerification(i.X, i.Y));
                }
            }
            else if (_vert.Count > 2)
            {
                foreach (var i in _vert)
                {
                    _hor.AddRange(HorVerification(i.X, i.Y));
                }
            }

            #region Уничтожение трех и более в ряду и присвоение бонуса
            int bonus_level = 0;

            if (_hor.Count < 3 && _vert.Count < 3)
                return;


            #region Уничтожение елементов
            //foreach (var item in _hor)
            //{
            //    if (this[item.X, item.Y] != 0)
            //    {
            //        if ((this[item.X, item.Y] % 10) != 0)
            //        {
            //            int bonus = (this[item.X, item.Y] % 10);
            //            int element = this[item.X, item.Y] - bonus;
            //            OnBonusDestruction?.Invoke(item.X, item.Y, bonus, element);
            //        }
            //        this[item.X, item.Y] -= this[item.X, item.Y];
            //        bonus_level++;
            //    }
            //}

            bonus_level += DestructionElements(_hor);

            //foreach (var item in _vert)
            //{
            //    if (this[item.X, item.Y] != 0)
            //    {
            //        if ((this[item.X, item.Y] % 10) != 0)
            //        {
            //            //Уничтожение клетки с бонусом, активация бонуса
            //        }
            //        this[item.X, item.Y] -= this[item.X, item.Y];
            //        bonus_level++;
            //    }
            //}

            bonus_level += DestructionElements(_vert);
            #endregion
            OnDestruction?.Invoke(bonus_level);
            if (bonus_level > 9)
                bonus_level = 9;

            if (bonus_level > 3)
                this[x, y] = bonus_level;

            #endregion

            
            _hor.Clear();
            _vert.Clear();

        }

        private int DestructionElements(List<Point> elements)
        {
            int result = 0;
            foreach (var item in elements)
            {
                int bonus = (this[item.X, item.Y] % 10);
                int element = this[item.X, item.Y] - bonus;
                if (this[item.X, item.Y] != 0)
                {
                    this[item.X, item.Y] -= this[item.X, item.Y];
                    if (bonus != 0)
                    {
                        OnBonusDestruction?.Invoke(item.X, item.Y, bonus, element);
                    }
                    result++;
                }
            }
            return result;
        }

        private List<Point> VertVerification(int x, int y)
        {
            int value = this[x, y];
            List<Point> result = new();
            result.Add(new Point(x, y));
            for (int i = y + 1; i < Rows; i++)
            {
                if (this[x, i] == value)
                {
                    result.Add(new Point(x, i));
                }
                else break;
            }

            for (int i = y - 1; i >= 0; i--)
            {
                if (this[x, i] == value)
                {
                    result.Add(new Point(x, i));
                }
                else break;
            }
            if (result.Count < 3)
                result.Clear();

            return result;
        }

        private List<Point> HorVerification(int x, int y)
        {
            int value = this[x, y];
            List<Point> result = new();
            result.Add(new Point(x, y));
            for (int i = x + 1; i < Columns; i++)
            {
                if (this[i, y] == value)
                {
                    result.Add(new Point(i, y));
                }
                else break;
            }

            for (int i = x - 1; i >= 0; i--)
            {
                if (this[i, y] == value)
                {
                    result.Add(new Point(i, y));
                }
                else break;
            }

            if (result.Count < 3)
                result.Clear();

            return result;
        }

        public bool CanMove(int x, int y)
        {
            bool result = false;

            result = (HorVerification(x, y).Count > 2) || (VertVerification(x, y).Count > 2);

            return result;
        }

        public void DestructionAllScreen()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    if (this[x, y] >= 10)
                    {
                        if (CanMove(x, y))
                            Destruction(x, y);
                    }
                }
            }
        }

        public void Move(Point first, Point second)
        {
            int tmp = this[first.X, first.Y];
            this[first.X, first.Y] = this[second.X, second.Y];
            this[second.X, second.Y] = tmp;
        }

        public void Fall()
        {
            for (int y = Rows - 1; y >= 0; y--)
            {
                for (int x = 0; x < Columns; x++)
                {
                    if (this[x, y] < 10)
                    {
                        for (int i = y; i >= 0; i--)
                        {
                            if (this[x, i] != 0)
                            {
                                this[x, y] += this[x, i];
                                this[x, i] = 0;
                                break;
                            }
                        }
                        if (this[x, y] < 10)
                        {
                            int bonus = this[x, y];
                            GenerateElement(x, y);
                            this[x, y] += bonus;
                        }
                    }
                }
            }
        }
    }
}
