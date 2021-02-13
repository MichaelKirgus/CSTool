// Copyright (C) 2019-2021 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
// Additional copyright notices in project base directory or main executable directory.
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static WeifenLuo.WinFormsUI.Docking.DockPanelExtender;

namespace WeifenLuo.WinFormsUI.ThemeVS2005
{
    internal class VS2005DockWindowFactory : IDockWindowFactory
    {
        public DockWindow CreateDockWindow(DockPanel dockPanel, DockState dockState)
        {
            return new VS2005DockWindow(dockPanel, dockState);
        }

        /// <summary>
        /// Dock window of Visual Studio 2005 theme.
        /// </summary>
        [ToolboxItem(false)]
        private class VS2005DockWindow : DockWindow
        {
            internal VS2005DockWindow(DockPanel dockPanel, DockState dockState) : base(dockPanel, dockState)
            {
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                // if DockWindow is document, draw the border
                if (DockState == DockState.Document)
                    e.Graphics.DrawRectangle(SystemPens.ControlDark, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);

                base.OnPaint(e);
            }
        }
    }
}