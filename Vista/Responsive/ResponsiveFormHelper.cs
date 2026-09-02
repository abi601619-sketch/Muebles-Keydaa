using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Vista.Responsive
{
    /// <summary>
    /// Makes WinForms screens adapt to the available client area.
    /// Existing Dock/Anchor/LayoutPanel behavior is preserved; controls that
    /// use the default Top|Left anchor are resized proportionally.
    /// </summary>
    public static class ResponsiveFormHelper
    {
        private sealed class Snapshot
        {
            public Control Control;
            public Rectangle Bounds;
            public Size ParentClientSize;
        }

        private sealed class State
        {
            public Form Form;
            public Size BaseClientSize;
            public List<Snapshot> Items = new List<Snapshot>();
            public bool Updating;
        }

        private static readonly Dictionary<Form, State> States = new Dictionary<Form, State>();

        public static void Apply(Form form)
        {
            if (form == null || States.ContainsKey(form))
                return;

            // A maximizable borderless form must not have a fixed MaximumSize.
            form.MaximumSize = Size.Empty;
            form.MinimumSize = new Size(
                Math.Min(form.ClientSize.Width, 900),
                Math.Min(form.ClientSize.Height, 500));

            State state = new State
            {
                Form = form,
                BaseClientSize = form.ClientSize
            };

            Capture(form, state);
            States.Add(form, state);

            form.Resize += delegate { ResizeForm(state); };
            form.FormClosed += delegate
            {
                form.Resize -= delegate { ResizeForm(state); };
                States.Remove(form);
            };
        }

        private static void Capture(Control parent, State state)
        {
            foreach (Control child in parent.Controls)
            {
                state.Items.Add(new Snapshot
                {
                    Control = child,
                    Bounds = child.Bounds,
                    ParentClientSize = parent.ClientSize
                });

                if (child.Controls.Count > 0)
                    Capture(child, state);
            }
        }

        private static void ResizeForm(State state)
        {
            if (state.Updating || state.Form.IsDisposed)
                return;

            state.Updating = true;
            try
            {
                foreach (Snapshot item in state.Items)
                {
                    Control c = item.Control;
                    if (c.IsDisposed || c.Parent == null)
                        continue;

                    // Let WinForms handle controls that already have an explicit
                    // Dock/Anchor/LayoutPanel arrangement.
                    if (c.Dock != DockStyle.None)
                        continue;

                    AnchorStyles explicitAnchors = c.Anchor;
                    bool defaultTopLeft =
                        (explicitAnchors & AnchorStyles.Left) != 0 &&
                        (explicitAnchors & AnchorStyles.Top) != 0 &&
                        (explicitAnchors & AnchorStyles.Right) == 0 &&
                        (explicitAnchors & AnchorStyles.Bottom) == 0;

                    if (!defaultTopLeft)
                        continue;

                    Size parentSize = c.Parent.ClientSize;
                    if (item.ParentClientSize.Width <= 0 || item.ParentClientSize.Height <= 0)
                        continue;

                    int x = Scale(item.Bounds.X, parentSize.Width, item.ParentClientSize.Width);
                    int y = Scale(item.Bounds.Y, parentSize.Height, item.ParentClientSize.Height);
                    int w = Scale(item.Bounds.Width, parentSize.Width, item.ParentClientSize.Width);
                    int h = Scale(item.Bounds.Height, parentSize.Height, item.ParentClientSize.Height);

                    w = Math.Max(1, w);
                    h = Math.Max(1, h);

                    c.Bounds = new Rectangle(x, y, w, h);
                }
            }
            finally
            {
                state.Updating = false;
            }
        }

        private static int Scale(int value, int current, int original)
        {
            return (int)Math.Round(value * (double)current / original);
        }
    }
}
