// Copyright (C) 2019-2021 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
// Additional copyright notices in project base directory or main executable directory.
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace WeifenLuo.WinFormsUI.ThemeVS2013
{
    public class VS2013DockPaneFactory : DockPanelExtender.IDockPaneFactory
    {
        public DockPane CreateDockPane(IDockContent content, DockState visibleState, bool show)
        {
            return new VS2013DockPane(content, visibleState, show);
        }

        [SuppressMessage("Microsoft.Naming", "CA1720:AvoidTypeNamesInParameters", MessageId = "1#")]
        public DockPane CreateDockPane(IDockContent content, FloatWindow floatWindow, bool show)
        {
            return new VS2013DockPane(content, floatWindow, show);
        }

        public DockPane CreateDockPane(IDockContent content, DockPane previousPane, DockAlignment alignment, double proportion, bool show)
        {
            return new VS2013DockPane(content, previousPane, alignment, proportion, show);
        }

        [SuppressMessage("Microsoft.Naming", "CA1720:AvoidTypeNamesInParameters", MessageId = "1#")]
        public DockPane CreateDockPane(IDockContent content, Rectangle floatWindowBounds, bool show)
        {
            return new VS2013DockPane(content, floatWindowBounds, show);
        }
    }
}
