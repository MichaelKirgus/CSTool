// Copyright (C) 2019-2021 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
// Additional copyright notices in project base directory or main executable directory.
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static WeifenLuo.WinFormsUI.Docking.DockPanel;
using static WeifenLuo.WinFormsUI.Docking.DockPanelExtender;

namespace WeifenLuo.WinFormsUI.ThemeVS2005
{
    public class VS2005PaneIndicatorFactory : IPaneIndicatorFactory
    {
        public IPaneIndicator CreatePaneIndicator(ThemeBase theme)
        {
            return new VS2005PaneIndicator();
        }

        [ToolboxItem(false)]
        internal class VS2005PaneIndicator : PictureBox, IPaneIndicator
        {
            private static Bitmap _bitmapPaneDiamond = Resources.DockIndicator_PaneDiamond;
            private static Bitmap _bitmapPaneDiamondLeft = Resources.DockIndicator_PaneDiamond_Left;
            private static Bitmap _bitmapPaneDiamondRight = Resources.DockIndicator_PaneDiamond_Right;
            private static Bitmap _bitmapPaneDiamondTop = Resources.DockIndicator_PaneDiamond_Top;
            private static Bitmap _bitmapPaneDiamondBottom = Resources.DockIndicator_PaneDiamond_Bottom;
            private static Bitmap _bitmapPaneDiamondFill = Resources.DockIndicator_PaneDiamond_Fill;
            private static Bitmap _bitmapPaneDiamondHotSpot = Resources.DockIndicator_PaneDiamond_HotSpot;
            private static Bitmap _bitmapPaneDiamondHotSpotIndex = Resources.DockIndicator_PaneDiamond_HotSpotIndex;
            private static HotSpotIndex[] _hotSpots =
            {
                new HotSpotIndex(1, 0, DockStyle.Top),
                new HotSpotIndex(0, 1, DockStyle.Left),
                new HotSpotIndex(1, 1, DockStyle.Fill),
                new HotSpotIndex(2, 1, DockStyle.Right),
                new HotSpotIndex(1, 2, DockStyle.Bottom)
            };

            private GraphicsPath _displayingGraphicsPath = DrawHelper.CalculateGraphicsPathFromBitmap(_bitmapPaneDiamond);

            public VS2005PaneIndicator()
            {
                SizeMode = PictureBoxSizeMode.AutoSize;
                Image = _bitmapPaneDiamond;
                Region = new Region(DisplayingGraphicsPath);
            }

            public GraphicsPath DisplayingGraphicsPath
            {
                get { return _displayingGraphicsPath; }
            }

            public DockStyle HitTest(Point pt)
            {
                if (!Visible)
                    return DockStyle.None;

                pt = PointToClient(pt);
                if (!ClientRectangle.Contains(pt))
                    return DockStyle.None;

                for (int i = _hotSpots.GetLowerBound(0); i <= _hotSpots.GetUpperBound(0); i++)
                {
                    if (_bitmapPaneDiamondHotSpot.GetPixel(pt.X, pt.Y) == _bitmapPaneDiamondHotSpotIndex.GetPixel(_hotSpots[i].X, _hotSpots[i].Y))
                        return _hotSpots[i].DockStyle;
                }

                return DockStyle.None;
            }

            private DockStyle m_status = DockStyle.None;
            public DockStyle Status
            {
                get { return m_status; }
                set
                {
                    m_status = value;
                    if (m_status == DockStyle.None)
                        Image = _bitmapPaneDiamond;
                    else if (m_status == DockStyle.Left)
                        Image = _bitmapPaneDiamondLeft;
                    else if (m_status == DockStyle.Right)
                        Image = _bitmapPaneDiamondRight;
                    else if (m_status == DockStyle.Top)
                        Image = _bitmapPaneDiamondTop;
                    else if (m_status == DockStyle.Bottom)
                        Image = _bitmapPaneDiamondBottom;
                    else if (m_status == DockStyle.Fill)
                        Image = _bitmapPaneDiamondFill;
                }
            }
        }
    }
}
