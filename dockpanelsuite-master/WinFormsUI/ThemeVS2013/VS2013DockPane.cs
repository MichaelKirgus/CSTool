// Copyright (C) 2019-2021 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
// Additional copyright notices in project base directory or main executable directory.
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace WeifenLuo.WinFormsUI.ThemeVS2013
{
    [ToolboxItem(false)]
    public class VS2013DockPane : DockPane
    {
        public VS2013DockPane(IDockContent content, DockState visibleState, bool show)
            : base(content, visibleState, show)
        {
        }

        [SuppressMessage("Microsoft.Naming", "CA1720:AvoidTypeNamesInParameters", MessageId = "1#")]
        public VS2013DockPane(IDockContent content, FloatWindow floatWindow, bool show)
            : base(content, floatWindow, show)
        {
        }

        public VS2013DockPane(IDockContent content, DockPane previousPane, DockAlignment alignment, double proportion, bool show)
            : base(content, previousPane, alignment, proportion, show)
        {
        }

        [SuppressMessage("Microsoft.Naming", "CA1720:AvoidTypeNamesInParameters", MessageId = "1#")]
        public VS2013DockPane(IDockContent content, Rectangle floatWindowBounds, bool show)
            : base(content, floatWindowBounds, show)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var color = DockPanel.Theme.ColorPalette.ToolWindowBorder;
            e.Graphics.FillRectangle(DockPanel.Theme.PaintingService.GetBrush(color), e.ClipRectangle);
        }

        protected override Rectangle ContentRectangle
        {
            get
            {
                var rect = base.ContentRectangle;
                if (DockState == DockState.Document || Contents.Count == 1)
                {
                    rect.Height--;
                }

                rect.Width -= 2;
                rect.X++;
                return rect;
            }
        }
    }
}
