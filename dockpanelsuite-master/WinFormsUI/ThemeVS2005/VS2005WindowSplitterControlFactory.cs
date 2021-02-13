// Copyright (C) 2019-2021 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
// Additional copyright notices in project base directory or main executable directory.
using System.ComponentModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static WeifenLuo.WinFormsUI.Docking.DockPanelExtender;

namespace WeifenLuo.WinFormsUI.ThemeVS2005
{
    internal class VS2005WindowSplitterControlFactory : IWindowSplitterControlFactory
    {
        public SplitterBase CreateSplitterControl(ISplitterHost host)
        {
            return new VS2005WindowSplitterControl(host);
        }

        [ToolboxItem(false)]
        private class VS2005WindowSplitterControl : SplitterBase
        {
            private ISplitterHost _host;

            public VS2005WindowSplitterControl(ISplitterHost host)
            {
                _host = host;
            }

            protected override int SplitterSize
            {
                get { return _host.DockPanel.Theme.Measures.SplitterSize; }
            }

            protected override void StartDrag()
            {
                _host.DockPanel.BeginDrag(_host, ((Control)_host).RectangleToScreen(Bounds));
            }
        }
    }
}
