// Copyright (C) 2019-2021 Michael Kirgus
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
// Additional copyright notices in project base directory or main executable directory.
using System.Drawing;

namespace WeifenLuo.WinFormsUI.Docking
{
    using ThemeVS2012;
    using ThemeVS2013;

    /// <summary>
    /// Visual Studio 2012 Light theme.
    /// </summary>
    public class VS2012BlueTheme : VS2012ThemeBase
    {
        public VS2012BlueTheme()
            : base(Decompress(Resources.vs2012blue_vstheme), new VS2013DockPaneSplitterControlFactory(), new VS2013WindowSplitterControlFactory())
        {
            ColorPalette.TabSelectedInactive.Background = ColorTranslator.FromHtml("#FF3D5277");// TODO: from theme .FromHtml("#FF4D6082");
        }
    }
}