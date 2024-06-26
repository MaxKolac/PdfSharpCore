#region PDFsharp Charting - A .NET charting library based on PDFsharp
//
// Authors:
//   Niklas Schneider (mailto:Niklas.Schneider@PdfSharpCore.com)
//
// Copyright (c) 2005-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.PdfSharpCore.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using PdfSharpCore.Drawing;

namespace PdfSharpCore.Charting.Renderers
{
    /// <summary>
    /// Represents the legend renderer specific to bar charts.
    /// </summary>
    internal class BarClusteredLegendRenderer : ColumnLikeLegendRenderer
    {
        /// <summary>
        /// Initializes a new instance of the BarClusteredLegendRenderer class with the
        /// specified renderer parameters.
        /// </summary>
        internal BarClusteredLegendRenderer(RendererParameters parms)
          : base(parms)
        {
        }

        /// <summary>
        /// Draws the legend.
        /// </summary>
        internal override void Draw()
        {
            ChartRendererInfo cri = (ChartRendererInfo)this.rendererParms.RendererInfo;
            LegendRendererInfo lri = cri.legendRendererInfo;
            if (lri == null)
                return;

            XGraphics gfx = this.rendererParms.Graphics;
            RendererParameters parms = new RendererParameters();
            parms.Graphics = gfx;

            LegendEntryRenderer ler = new LegendEntryRenderer(parms);

            bool verticalLegend = (lri.legend.docking == DockingType.Left || lri.legend.docking == DockingType.Right);
            int paddingFactor = 1;
            if (lri.BorderPen != null)
                paddingFactor = 2;
            XRect legendRect = lri.Rect;
            legendRect.X += LegendRenderer.LeftPadding * paddingFactor;
            if (verticalLegend)
                legendRect.Y = legendRect.Bottom - LegendRenderer.BottomPadding * paddingFactor;
            else
                legendRect.Y += LegendRenderer.TopPadding * paddingFactor;

            foreach (LegendEntryRendererInfo leri in cri.legendRendererInfo.Entries)
            {
                if (verticalLegend)
                    legendRect.Y -= leri.Height;

                XRect entryRect = legendRect;
                entryRect.Width = leri.Width;
                entryRect.Height = leri.Height;

                leri.Rect = entryRect;
                parms.RendererInfo = leri;
                ler.Draw();

                if (verticalLegend)
                    legendRect.Y -= LegendRenderer.EntrySpacing;
                else
                    legendRect.X += entryRect.Width + LegendRenderer.EntrySpacing;
            }

            // Draw border around legend
            if (lri.BorderPen != null)
            {
                XRect borderRect = lri.Rect;
                borderRect.X += LegendRenderer.LeftPadding;
                borderRect.Y += LegendRenderer.TopPadding;
                borderRect.Width -= LegendRenderer.LeftPadding + LegendRenderer.RightPadding;
                borderRect.Height -= LegendRenderer.TopPadding + LegendRenderer.BottomPadding;
                gfx.DrawRectangle(lri.BorderPen, borderRect);
            }
        }
    }
}
