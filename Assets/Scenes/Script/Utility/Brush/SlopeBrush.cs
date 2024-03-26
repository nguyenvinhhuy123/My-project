using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

namespace UnityEditor.Tilemaps
{
    /// <summary>
    /// This Brush helps draw slope of Tiles onto a Tilemap.
    /// </summary>
    [CustomGridBrush(true, false, false, "Slope Brush")]
    
    public class SlopeBrush : LineBrush
    {
        /// <summary>
        /// False mean fill all point under the base line
        /// True mean fill all point above the base line
        /// </summary>
        [Tooltip("Hold Ctrl key to fill the upper points of the base line")]
        public bool reverse = false;
        /// <summary>
        /// Paints tiles and GameObjects into a given position within the selected layers.
        /// The LineBrush overrides this to provide line painting functionality.
        /// The first paint action sets the starting point of the line.
        /// The next paint action sets the ending point of the line and paints Tile from start to end.
        /// </summary>
        /// <param name="grid">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the paint operation. By default the currently selected GameObject.</param>
        /// <param name="position">The coordinates of the cell to paint data to.</param>
        public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            if (lineStartActive)
            {
                Vector2Int startPos = new Vector2Int(lineStart.x, lineStart.y);
                Vector2Int endPos = new Vector2Int(position.x, position.y);
                if (startPos == endPos)
                    base.Paint(grid, brushTarget, position);    
                else
                {
                    foreach (var point in GetPointOnSlope(startPos, endPos, fillGaps, reverse))
                    {
                        Vector3Int paintPos = new Vector3Int(point.x, point.y, position.z);
                        base.Paint(grid, brushTarget, paintPos);
                    }
                }
                lineStartActive = false;
            }
            else if (IsMoving)
            {
                base.Paint(grid, brushTarget, position);
            }
            else
            {
                lineStart = position;
                lineStartActive = true;
            }
        }

        /// <summary>
        /// Enumerates all the points between the start and end position which are
        /// linked diagonally or orthogonally. (base line)
        /// Enumerates all the points below or above the baseline point to create a triangle shape slope
        /// </summary>
        /// <param name="startPos">Start position of the line.</param>
        /// <param name="endPos">End position of the line.</param>
        /// <param name="fillGaps">Fills any gaps between the start and end position so that
        /// all points are linked only orthogonally.</param>
        /// <param name="reverse">False mean fill all point under the base line
        /// True mean fill all point above the base line</param>
        /// <returns>Returns an IEnumerable which enumerates all the points between the start and end position which are
        /// linked diagonally or orthogonally (base line) 
        /// and all the points below or above the baseline to create a triangle shape slope</returns>
        public static IEnumerable<Vector2Int> GetPointOnSlope(Vector2Int startPos, Vector2Int endPos, bool fillGaps, bool reverse)
        {
            //* Well I figure fillGaps var is not needed for slope brush, so set it to false regardless */
            var baseLinePoints = GetPointsOnLine(startPos, endPos, false);
            int slope_y_bound;
            var result = baseLinePoints;
            if (reverse)
            {
                /* Take max too set bound to the highest point y axis, 
                this mean fill all the points above base line */
                slope_y_bound = Mathf.Max(startPos.y, endPos.y);
            }
            else 
            {
                /* Take min too set bound to the lowest point y axis, 
                this mean fill all the points below base line */
                slope_y_bound = Mathf.Min(startPos.y, endPos.y);
            }
            foreach ( Vector2Int point in baseLinePoints)
            {
                Vector2Int new_end_point = new Vector2Int(point.x, slope_y_bound);
                var fillPoints = GetPointsOnLine(point, new_end_point);
                fillPoints = fillPoints.Except(new [] {point});
                result = result.Union(fillPoints);
            }
            return result;
        }
    }

    /// <summary>
    /// The Brush Editor for a Slope Brush.
    /// </summary>
    [CustomEditor(typeof(SlopeBrush))]
    public class SlopeBrushEditor : GridBrushEditor
    {
        private SlopeBrush slopeBrush { get { return target as SlopeBrush; } }
        private Tilemap lastTilemap;

        /// <summary>
        /// Callback for painting the GUI for the GridBrush in the Scene View.
        /// The CoordinateBrush Editor overrides this to draw the preview of the brush when drawing lines.
        /// </summary>
        /// <param name="grid">Grid that the brush is being used on.</param>
        /// <param name="brushTarget">Target of the GridBrushBase::ref::Tool operation. By default the currently selected GameObject.</param>
        /// <param name="position">Current selected location of the brush.</param>
        /// <param name="tool">Current GridBrushBase::ref::Tool selected.</param>
        /// <param name="executing">Whether brush is being used.</param>
        public override void OnPaintSceneGUI(GridLayout grid, GameObject brushTarget, BoundsInt position, GridBrushBase.Tool tool, bool executing)
        {
            base.OnPaintSceneGUI(grid, brushTarget, position, tool, executing);
            if (slopeBrush.lineStartActive && brushTarget != null)
            {
                Tilemap tilemap = brushTarget.GetComponent<Tilemap>();
                if (tilemap != null)
                {
                    tilemap.ClearAllEditorPreviewTiles();
                    lastTilemap = tilemap;
                }

                // Draw preview tiles for tilemap
                Vector2Int startPos = new Vector2Int(slopeBrush.lineStart.x, slopeBrush.lineStart.y);
                Vector2Int endPos = new Vector2Int(position.x, position.y);
                if (startPos == endPos)
                    PaintPreview(grid, brushTarget, position.min);
                else
                {
                    if(Event.current.control)
                    {
                        slopeBrush.reverse = true;
                    }
                    else 
                    {
                        slopeBrush.reverse = false;
                    }
                    foreach (var point in SlopeBrush.GetPointOnSlope(startPos, endPos, slopeBrush.fillGaps, slopeBrush.reverse))
                    {
                        Vector3Int paintPos = new Vector3Int(point.x, point.y, position.z);
                        PaintPreview(grid, brushTarget, paintPos);
                    }
                }

                if (Event.current.type == EventType.Repaint)
                {
                    var min = slopeBrush.lineStart;
                    var max = slopeBrush.lineStart + position.size;

                    // Draws a box on the picked starting position
                    GL.PushMatrix();
                    GL.MultMatrix(GUI.matrix);
                    GL.Begin(GL.LINES);
                    Handles.color = Color.blue;
                    Handles.DrawLine(new Vector3(min.x, min.y, min.z), new Vector3(max.x, min.y, min.z));
                    Handles.DrawLine(new Vector3(max.x, min.y, min.z), new Vector3(max.x, max.y, min.z));
                    Handles.DrawLine(new Vector3(max.x, max.y, min.z), new Vector3(min.x, max.y, min.z));
                    Handles.DrawLine(new Vector3(min.x, max.y, min.z), new Vector3(min.x, min.y, min.z));
                    GL.End();
                    GL.PopMatrix();
                }
            }
        }

        /// <summary>
        /// Clears all line previews.
        /// </summary>
        public override void ClearPreview()
        {
            base.ClearPreview();
            if (lastTilemap != null)
            {
                lastTilemap.ClearAllEditorPreviewTiles();
                lastTilemap = null;
            }
        }
    }
}

