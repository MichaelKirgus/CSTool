// Copyright (C) 2019-2021 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
// Additional copyright notices in project base directory or main executable directory.
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace WeifenLuo.WinFormsUI.ThemeVS2013
{
    [ToolboxItem(false)]
    internal class VS2013SplitterControl : DockPane.SplitterControlBase
    {
        private readonly SolidBrush _horizontalBrush;
        private int SplitterSize { get; }

        public VS2013SplitterControl(DockPane pane)
            : base(pane)
        {
            _horizontalBrush = pane.DockPanel.Theme.PaintingService.GetBrush(pane.DockPanel.Theme.ColorPalette.MainWindowActive.Background);
            SplitterSize = pane.DockPanel.Theme.Measures.SplitterSize;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0)
                return;

            switch (Alignment)
            {
                case DockAlignment.Right:
                case DockAlignment.Left:
                    {
                        Debug.Assert(SplitterSize == rect.Width);
                        e.Graphics.FillRectangle(_horizontalBrush, rect);
                    }
                    break;
                case DockAlignment.Bottom:
                case DockAlignment.Top:
                    {
                        Debug.Assert(SplitterSize == rect.Height);
                        e.Graphics.FillRectangle(_horizontalBrush, rect);
                    }
                    break;
            }
        }
    }
}