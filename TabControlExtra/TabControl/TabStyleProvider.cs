/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Adiict.UI.Forms
{
    [System.ComponentModel.ToolboxItem(false)]
	public abstract class TabStyleProvider : Component
	{
		private const int TabCloserPathInset = 4;

		#region Constructor
		
		protected TabStyleProvider(TabControlExtra tabControl){
			TabControl = tabControl;
			
			_FocusColor = Color.Orange;
			
			if (TabControl.RightToLeftLayout){
				_ImageAlign = ContentAlignment.MiddleRight;
			} else {
				_ImageAlign = ContentAlignment.MiddleLeft;
			}
			
			HotTrack = true;

            Padding = new Point(6, 3);

		}
		
		#endregion

		#region Factory Methods
		
		public static TabStyleProvider CreateProvider(TabControlExtra tabControl){
			TabStyleProvider provider;
			
			//	Depending on the display style of the tabControl generate an appropriate provider.
			switch (tabControl.DisplayStyle) {
				case TabStyle.None:
					provider = new TabStyleNoneProvider(tabControl);
					break;
					
				case TabStyle.Default:
					provider = new TabStyleDefaultProvider(tabControl);
					break;
					
				case TabStyle.Angled:
					provider = new TabStyleAngledProvider(tabControl);
					break;
					
				case TabStyle.Rounded:
					provider = new TabStyleRoundedProvider(tabControl);
					break;
					
				case TabStyle.VisualStudio:
					provider = new TabStyleVisualStudioProvider(tabControl);
					break;
					
				case TabStyle.Chrome:
					provider = new TabStyleChromeProvider(tabControl);
					break;
					
				case TabStyle.IE8:
					provider = new TabStyleIE8Provider(tabControl);
					break;

				case TabStyle.VS2010:
					provider = new TabStyleVS2010Provider(tabControl);
					break;

                case TabStyle.Rectangular:
                    provider = new TabStyleRectangularProvider(tabControl);
                    break;

                case TabStyle.VS2012:
                    provider = new TabStyleVS2012Provider(tabControl);
                    break;

            default:
					provider = new TabStyleDefaultProvider(tabControl);
					break;
			}
			
			provider._Style = tabControl.DisplayStyle;
			return provider;
		}
		
		#endregion
		
		#region	Instance variables

        protected TabControlExtra TabControl { get; private set; }

		private Point _Padding;
		private bool _HotTrack;
		private TabStyle _Style = TabStyle.Default;
		
		
		private ContentAlignment _ImageAlign;
		private int _Radius = 1;
		private int _Overlap;
		private bool _FocusTrack;
		private float _Opacity = 1;
		private bool _ShowTabCloser;
        private bool _SelectedTabIsLarger;

        private BlendStyle _BlendStyle = BlendStyle.Normal;

        private Color _BorderColorDisabled = Color.Empty;
        private Color _BorderColorFocused = Color.Empty;
		private Color _BorderColorHighlighted = Color.Empty;
        private Color _BorderColorSelected = Color.Empty;
        private Color _BorderColorUnselected = Color.Empty;

        private Color _CloserColorFocused = SystemColors.ControlDark;
        private Color _CloserColorFocusedActive = SystemColors.ControlDark;
        private Color _CloserColorSelected = SystemColors.ControlDark;
        private Color _CloserColorSelectedActive = SystemColors.ControlDark;
        private Color _CloserColorHighlighted = SystemColors.ControlDark;
        private Color _CloserColorHighlightedActive = SystemColors.ControlDark;
        private Color _CloserColorUnselected = Color.Empty;

        private Color _CloserButtonFillColorFocused = Color.Empty;
        private Color _CloserButtonFillColorFocusedActive = Color.Empty;
        private Color _CloserButtonFillColorSelected = Color.Empty;
        private Color _CloserButtonFillColorSelectedActive = Color.Empty;
        private Color _CloserButtonFillColorHighlighted = Color.Empty;
        private Color _CloserButtonFillColorHighlightedActive = Color.Empty;
        private Color _CloserButtonFillColorUnselected = Color.Empty;

        private Color _CloserButtonOutlineColorFocused = Color.Empty;
        private Color _CloserButtonOutlineColorFocusedActive = Color.Empty;
        private Color _CloserButtonOutlineColorSelected = Color.Empty;
        private Color _CloserButtonOutlineColorSelectedActive = Color.Empty;
        private Color _CloserButtonOutlineColorHighlighted = Color.Empty;
        private Color _CloserButtonOutlineColorHighlightedActive = Color.Empty;
        private Color _CloserButtonOutlineColorUnselected = Color.Empty;

		private Color _FocusColor = Color.Empty;

        private Color _PageBackgroundColorDisabled = Color.Empty;
        private Color _PageBackgroundColorFocused = Color.Empty;
        private Color _PageBackgroundColorHighlighted = Color.Empty;
        private Color _PageBackgroundColorSelected = Color.Empty;
        private Color _PageBackgroundColorUnselected = Color.Empty;

        private Color _TabColorDisabled1 = Color.Empty;
        private Color _TabColorDisabled2 = Color.Empty;
        private Color _TabColorFocused1 = Color.Empty;
        private Color _TabColorFocused2 = Color.Empty;
        private Color _TabColorSelected1 = Color.Empty;
        private Color _TabColorSelected2 = Color.Empty;
        private Color _TabColorUnselected1 = Color.Empty;
        private Color _TabColorUnselected2 = Color.Empty;
        private Color _TabColorHighlighted1 = Color.Empty;
        private Color _TabColorHighlighted2 = Color.Empty;

        private Color _TextColorDisabled = Color.Empty;
        private Color _TextColorFocused = Color.Empty;
        private Color _TextColorHighlighted = Color.Empty;
        private Color _TextColorSelected = Color.Empty;
        private Color _TextColorUnselected = Color.Empty;
		
        private Padding _TabPageMargin = new Padding(1);

        private int _TabPageRadius = 0;
		
		#endregion
		
		#region overridable Methods
		
		public virtual void AddTabBorder(GraphicsPath path, Rectangle tabBounds) {
            switch (TabControl.Alignment) {
                case TabAlignment.Top:
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y);
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom);
                    break;
                case TabAlignment.Bottom:
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom);
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y);
                    break;
                case TabAlignment.Left:
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y);
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    break;
                case TabAlignment.Right:
                    path.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right, tabBounds.Y);
                    path.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom);
                    path.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
                    break;
            }
        }

        public virtual Rectangle GetTabRect(Rectangle baseTabRect, Rectangle pageBounds, bool tabIsSelected) {
            var tabRect = baseTabRect;

            //	Adjust to meet the tabpage
            switch (TabControl.Alignment) {
                case TabAlignment.Top:
                    tabRect.Height += pageBounds.Top - tabRect.Bottom;
                    break;
                case TabAlignment.Bottom:
                    tabRect.Height += tabRect.Top - pageBounds.Bottom;
                    tabRect.Y -= tabRect.Top - pageBounds.Bottom;
                    break;
                case TabAlignment.Left:
                    tabRect.Width += pageBounds.Left - tabRect.Right;
                    break;
                case TabAlignment.Right:
                    tabRect.Width += tabRect.Left - pageBounds.Right;
                    tabRect.X -= tabRect.Left - pageBounds.Right;
                    break;
            }

            if (SelectedTabIsLarger) tabRect = EnlargeTab(tabRect, tabIsSelected);

            //	Create Overlap
            if (TabControl.Alignment <= TabAlignment.Bottom) {
                tabRect.X -= _Overlap;
                tabRect.Width += _Overlap;
            } else {
                tabRect.Y -= _Overlap;
                tabRect.Height += _Overlap;
            }

            tabRect = EnsureTabIsInView(tabRect, pageBounds);

            return tabRect;
        }

        private Rectangle EnlargeTab(Rectangle tabBounds, bool tabIsSelected) {
            Rectangle newTabBounds = tabBounds;
            int widthIncrement = (int)(tabIsSelected ? 1 : 0);
            int heightIncrement = (int)(tabIsSelected ? 1 : -1);

            switch (TabControl.Alignment) {
                case TabAlignment.Top:
                    newTabBounds.Y -= heightIncrement;
                    newTabBounds.Height += heightIncrement;
                    newTabBounds.X -= widthIncrement;
                    newTabBounds.Width += 2 * widthIncrement;
                    break;
                case TabAlignment.Bottom:
                    newTabBounds.Height += heightIncrement;
                    newTabBounds.X -= widthIncrement;
                    newTabBounds.Width += 2 * widthIncrement;
                    break;
                case TabAlignment.Left:
                    newTabBounds.X -= heightIncrement;
                    newTabBounds.Width += heightIncrement;
                    newTabBounds.Y -= widthIncrement;
                    newTabBounds.Height += 2 * widthIncrement;
                    break;
                case TabAlignment.Right:
                    newTabBounds.Width += heightIncrement;
                    newTabBounds.Y -= widthIncrement;
                    newTabBounds.Height += 2 * widthIncrement;
                    break;
            }
            return newTabBounds;
        }

        protected virtual Rectangle EnsureTabIsInView(Rectangle tabBounds, Rectangle pageBounds) {
			//	Adjust tab to fit within the page bounds.
			//	Make sure we only reposition visible tabs, as we may have scrolled out of view.

            if (!TabControl.IsTabVisible(tabBounds, pageBounds)) return tabBounds;

            var newTabBounds = tabBounds;

            switch (TabControl.Alignment) {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    if (newTabBounds.X <= pageBounds.X + 4) newTabBounds.X = pageBounds.X;
                    newTabBounds.Intersect(new Rectangle(pageBounds.X, tabBounds.Y, pageBounds.Width, tabBounds.Height));
                    break;
                case TabAlignment.Left:
                case TabAlignment.Right:
                    if (newTabBounds.Y <= pageBounds.Y + 4) newTabBounds.Y = pageBounds.Y;
                    newTabBounds.Intersect(new Rectangle(tabBounds.X,pageBounds.Y,tabBounds.Width,pageBounds.Height));
                    break;
            }

            return newTabBounds;
		}

        #endregion
		
		#region	Base Properties

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TabStyle DisplayStyle {
			get { return _Style; }
			set { 
                _Style = value;
            }
		}

        [Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BlendStyle BlendStyle {
            get { return _BlendStyle; }
            set {
                _BlendStyle = value;
                TabControl.Invalidate();
            }
        }

        [Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public ContentAlignment ImageAlign {
			get { return _ImageAlign; }
			set {
				_ImageAlign = value;
			}
		}
		
		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Point Padding {
			get { return _Padding; }
			set {
				_Padding = value;
				if (_ShowTabCloser){
					if (value.X + (int)(_Radius/2) < -TabControlExtra.TabCloserButtonSize){
						((TabControl)TabControl).Padding = new Point(0, value.Y);
					} else {
                        ((TabControl)TabControl).Padding = new Point(value.X + _Radius + (int)(TabControlExtra.TabCloserButtonSize + 10) / 2, value.Y);
					}
				} else {
					if (value.X + (int)(_Radius/2) < 1){
						((TabControl)TabControl).Padding = new Point(0, value.Y);
					} else {
                        ((TabControl)TabControl).Padding = new Point(value.X + _Radius, value.Y);
					}
				}
			}
		}


		[Category("Appearance"), DefaultValue(1), Browsable(true)]
		public int Radius {
			get { return _Radius; }
			set {
				if (value < 1) throw new ArgumentException("The radius cannot be less than 1", nameof(value));
				
				_Radius = value;
				//	Adjust padding
				Padding = _Padding;
			}
		}

		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int Overlap {
			get { return _Overlap; }
			set {
				if (value < 0){
					throw new ArgumentException("The tabs cannot have a negative overlap", nameof(value));
				}
				_Overlap = value;
			}
		}
		
		
		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool FocusTrack {
			get { return _FocusTrack; }
			set {
				_FocusTrack = value;
			}
		}
		
		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool HotTrack {
			get { return _HotTrack; }
			set {
				_HotTrack = value;
				((TabControl)TabControl).HotTrack = value;
			}
		}

        [Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool SelectedTabIsLarger {
            get { return _SelectedTabIsLarger; }
			set {
                _SelectedTabIsLarger = value;
                TabControl.Invalidate();
			}
		}
		
        [Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool ShowTabCloser {
			get { return _ShowTabCloser; }
			set {
				_ShowTabCloser = value;
				//	Adjust padding
				Padding = _Padding;
			}
		}

		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public float Opacity {
			get { return _Opacity; }
			set {
				if (value < 0 || value > 1) {
					throw new ArgumentException("The opacity must be between 0 and 1", nameof(value));
				}
				_Opacity = value;
			}
		}

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorDisabled {
            get {
                if (_BorderColorDisabled.IsEmpty) {
                    return SystemColors.ControlLight;
                } else {
                    return _BorderColorDisabled;
                }
            }
            set {
                if (value.Equals(SystemColors.ControlLight)) {
                    _BorderColorDisabled = Color.Empty;
                } else {
                    _BorderColorDisabled = value;
                }
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorFocused {
            get {
                if (_BorderColorFocused.IsEmpty) {
                    return ThemedColors.ToolBorder;
                } else {
                    return _BorderColorFocused;
                }
            }
            set {
                if (!value.Equals(BorderColorFocused)) {
                    if (value.Equals(ThemedColors.ToolBorder)) {
                        _BorderColorFocused = Color.Empty;
                    } else {
                        _BorderColorFocused = value;
                    }
                }
            }
        }

		[Category("Appearance"), DefaultValue(typeof(Color), "")]
		public Color BorderColorHighlighted
		{
			get {
				if (_BorderColorHighlighted.IsEmpty){
					return SystemColors.ControlDark;
				} else {
					return _BorderColorHighlighted;
				}
			}
			set {
				if (value.Equals(SystemColors.ControlDark)){
					_BorderColorHighlighted = Color.Empty;
				} else {
					_BorderColorHighlighted = value;
				}
			}
		}

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color BorderColorSelected {
            get {
                if (_BorderColorSelected.IsEmpty) {
                    return SystemColors.ControlDark;
                } else {
                    return _BorderColorSelected;
                }
            }
            set {
                if (value.Equals(SystemColors.ControlDark)) {
                    _BorderColorSelected = Color.Empty;
                } else {
                    _BorderColorSelected = value;
                }
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
		public Color BorderColorUnselected
		{
			get {
				if (_BorderColorUnselected.IsEmpty){
					return SystemColors.ControlDark;
				} else {
					return _BorderColorUnselected;
				}
			}
			set {
				if (value.Equals(SystemColors.ControlDark)){
					_BorderColorUnselected = Color.Empty;
				} else {
					_BorderColorUnselected = value;
				}
			}
		}

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color PageBackgroundColorDisabled {
            get {
                if (_PageBackgroundColorDisabled.IsEmpty) {
                    return SystemColors.Control;
                } else {
                    return _PageBackgroundColorDisabled;
                }
            }
            set {
                _PageBackgroundColorDisabled = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color PageBackgroundColorFocused {
            get {
                if (_PageBackgroundColorFocused.IsEmpty) {
                    return SystemColors.ControlLight;
                } else {
                    return _PageBackgroundColorFocused;
                }
            }
            set {
                _PageBackgroundColorFocused = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color PageBackgroundColorHighlighted {
            get {
                if (_PageBackgroundColorHighlighted.IsEmpty) {
                    return PageBackgroundColorUnselected;
                } else {
                    return _PageBackgroundColorHighlighted;
                }
            }
            set {
                _PageBackgroundColorHighlighted = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color PageBackgroundColorSelected {
            get {
                if (_PageBackgroundColorSelected.IsEmpty) {
                    return SystemColors.ControlLightLight;
                } else {
                    return _PageBackgroundColorSelected;
                }
            }
            set {
                _PageBackgroundColorSelected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color PageBackgroundColorUnselected {
            get {
                if (_PageBackgroundColorUnselected.IsEmpty) {
                    return SystemColors.Control;
                } else {
                    return _PageBackgroundColorUnselected;
                }
            }
            set {
                _PageBackgroundColorUnselected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorDisabled1 {
            get {
                if (_TabColorDisabled1.IsEmpty) {
                    return PageBackgroundColorDisabled;
                } else {
                    return _TabColorDisabled1;
                }
            }
            set {
                _TabColorDisabled1 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorDisabled2 {
            get {
                if (_TabColorDisabled2.IsEmpty) {
                    return TabColorDisabled1;
                } else {
                    return _TabColorDisabled2;
                }
            }
            set {
                _TabColorDisabled2 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorFocused1 {
            get {
                if (_TabColorFocused1.IsEmpty) {
                    return PageBackgroundColorFocused;
                } else {
                    return _TabColorFocused1;
                }
            }
            set {
                _TabColorFocused1 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorFocused2 {
            get {
                if (_TabColorFocused2.IsEmpty) {
                    return TabColorFocused1;
                } else {
                    return _TabColorFocused2;
                }
            }
            set {
                _TabColorFocused2 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorSelected1 {
            get {
                if (_TabColorSelected1.IsEmpty) {
                    return PageBackgroundColorSelected;
                } else {
                    return _TabColorSelected1;
                }
            }
            set {
                _TabColorSelected1 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorSelected2 {
            get {
                if (_TabColorSelected2.IsEmpty) {
                    return TabColorSelected1;
                } else {
                    return _TabColorSelected2;
                }
            }
            set {
                _TabColorSelected2 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorUnselected1 {
            get {
                if (_TabColorUnselected1.IsEmpty) {
                    return PageBackgroundColorUnselected;
                } else {
                    return _TabColorUnselected1;
                }
            }
            set {
                _TabColorUnselected1 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorUnselected2 {
            get {
                if (_TabColorUnselected2.IsEmpty) {
                    return TabColorUnselected1;
                } else {
                    return _TabColorUnselected2;
                }
            }
            set {
                _TabColorUnselected2 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorHighlighted1 {
            get {
                if (_TabColorHighlighted1.IsEmpty) {
                    return PageBackgroundColorHighlighted;
                } else {
                    return _TabColorHighlighted1;
                }
            }
            set {
                _TabColorHighlighted1 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TabColorHighlighted2 {
            get {
                if (_TabColorHighlighted2.IsEmpty) {
                    return TabColorHighlighted1;
                } else {
                    return _TabColorHighlighted2;
                }
            }
            set {
                _TabColorHighlighted2 = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorDisabled {
            get {
                if (_TextColorDisabled.IsEmpty) {
                    return SystemColors.ControlDark;
                } else {
                    return _TextColorDisabled;
                }
            }
            set {
                if (value.Equals(SystemColors.ControlDark)) {
                    _TextColorDisabled = Color.Empty;
                } else {
                    _TextColorDisabled = value;
                }
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorFocused {
            get {
                if (_TextColorFocused.IsEmpty) {
                    return TextColorSelected;
                } else {
                    return _TextColorFocused;
                }
            }
            set {
                _TextColorFocused = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorHighlighted {
            get {
                if (_TextColorHighlighted.IsEmpty) {
                    return TextColorUnselected;
                } else {
                    return _TextColorHighlighted;
                }
            }
            set {
                _TextColorHighlighted = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
		public Color TextColorSelected
		{
			get {
				if (_TextColorSelected.IsEmpty){
					return SystemColors.ControlText;
				} else {
					return _TextColorSelected;
				}
			}
			set {
				if (value.Equals(SystemColors.ControlText)){
					_TextColorSelected = Color.Empty;
				} else {
					_TextColorSelected = value;
				}
			}
		}

        [Category("Appearance"), DefaultValue(typeof(Color), "")]
        public Color TextColorUnselected {
            get {
                if (_TextColorUnselected.IsEmpty) {
                    return SystemColors.ControlText;
                } else {
                    return _TextColorUnselected;
                }
            }
            set {
                if (value.Equals(SystemColors.ControlText)) {
                    _TextColorUnselected = Color.Empty;
                } else {
                    _TextColorUnselected = value;
                }
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "Orange")]
		public Color FocusColor
		{
			get { return _FocusColor; }
			set { _FocusColor = value;
			}
		}

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorFocused {
            get { return _CloserColorFocused; }
            set {
                _CloserColorFocused = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorFocusedActive {
            get { return _CloserColorFocusedActive; }
            set {
                _CloserColorFocusedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorSelected {
            get { return _CloserColorSelected; }
            set {
                _CloserColorSelected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorSelectedActive {
            get { return _CloserColorSelectedActive; }
            set {
                _CloserColorSelectedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorHighlighted {
            get { return _CloserColorHighlighted; }
            set {
                _CloserColorHighlighted = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlDark")]
        public Color CloserColorHighlightedActive {
            get { return _CloserColorHighlightedActive; }
            set {
                _CloserColorHighlightedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserColorUnselected {
            get { return _CloserColorUnselected; }
            set {
                _CloserColorUnselected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorFocused {
            get { return _CloserButtonFillColorFocused; }
            set {
                _CloserButtonFillColorFocused = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorFocusedActive {
            get { return _CloserButtonFillColorFocusedActive; }
            set {
                _CloserButtonFillColorFocusedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorSelected {
            get { return _CloserButtonFillColorSelected ; }
            set {
                _CloserButtonFillColorSelected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorSelectedActive {
            get { return _CloserButtonFillColorSelectedActive; }
            set {
                _CloserButtonFillColorSelectedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorHighlighted {
            get { return _CloserButtonFillColorHighlighted; }
            set {
                _CloserButtonFillColorHighlighted = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorHighlightedActive {
            get { return _CloserButtonFillColorHighlightedActive; }
            set {
                _CloserButtonFillColorHighlightedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonFillColorUnselected {
            get { return _CloserButtonFillColorUnselected; }
            set {
                _CloserButtonFillColorUnselected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorFocused {
            get { return _CloserButtonOutlineColorFocused; }
            set {
                _CloserButtonOutlineColorFocused = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorFocusedActive {
            get { return _CloserButtonOutlineColorFocusedActive; }
            set {
                _CloserButtonOutlineColorFocusedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorSelected {
            get { return _CloserButtonOutlineColorSelected; }
            set {
                _CloserButtonOutlineColorSelected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorSelectedActive {
            get { return _CloserButtonOutlineColorSelectedActive; }
            set {
                _CloserButtonOutlineColorSelectedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorHighlighted {
            get { return _CloserButtonOutlineColorHighlighted; }
            set {
                _CloserButtonOutlineColorHighlighted = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorHighlightedActive {
            get { return _CloserButtonOutlineColorHighlightedActive; }
            set {
                _CloserButtonOutlineColorHighlightedActive = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "Empty")]
        public Color CloserButtonOutlineColorUnselected {
            get { return _CloserButtonOutlineColorUnselected; }
            set {
                _CloserButtonOutlineColorUnselected = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Padding), "{1,1,1,1}")]
        public Padding TabPageMargin 
        {
            get {return _TabPageMargin;}
            set {
                if (value.Left < 0) value.Left = 0;
                if (value.Right< 0) value.Right = 0;
                if (value.Top < 0) value.Top = 0;
                if (value.Bottom < 0) value.Bottom = 0;

                if (value.Left > 4) value.Left = 4;
                if (value.Right > 4) value.Right = 4;
                if (value.Top > 4) value.Top = 4;
                if (value.Bottom > 4) value.Bottom = 4;

                _TabPageMargin = value;
            }
        }

        [Category("Appearance"), DefaultValue(typeof(int), "0")]
        public int TabPageRadius {
            get { return _TabPageRadius; }
            set { 
                if (value < 0) value = 0;
                if (value > 4) value = 4;
                _TabPageRadius = value;
            }
        }

        #endregion

		#region Painting

        protected internal virtual void DrawTabCloser(GraphicsPath closerPath, GraphicsPath closerButtonPath, Graphics graphics, TabState state, Point mousePosition) {
            bool active = closerButtonPath.GetBounds().Contains(mousePosition);
            switch (state) {
                case TabState.Disabled:
                    DrawTabCloser(closerPath, closerButtonPath, graphics, CloserColorUnselected, CloserButtonFillColorUnselected, CloserButtonOutlineColorUnselected);
                    break;
                case TabState.Focused:
                    if (active) {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, CloserColorFocusedActive, CloserButtonFillColorFocusedActive, CloserButtonOutlineColorFocusedActive);
                    } else {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, CloserColorFocused, CloserButtonFillColorFocused, CloserButtonOutlineColorFocused);
                    }
                    break;
                case TabState.Highlighted:
                    if (active) {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, CloserColorHighlightedActive, CloserButtonFillColorHighlightedActive, CloserButtonOutlineColorHighlightedActive);
                    } else {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, CloserColorHighlighted, CloserButtonFillColorHighlighted, CloserButtonOutlineColorHighlighted);
                    }
                    break;
                case TabState.Selected:
                    if (active) {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, CloserColorSelectedActive, CloserButtonFillColorSelectedActive, CloserButtonOutlineColorSelectedActive);
                    } else {
                        DrawTabCloser(closerPath, closerButtonPath, graphics, CloserColorSelected, CloserButtonFillColorSelected, CloserButtonOutlineColorSelected);
                    }
                    break;
                case TabState.Unselected:
                    DrawTabCloser(closerPath, closerButtonPath, graphics, CloserColorUnselected, CloserButtonFillColorUnselected, CloserButtonOutlineColorUnselected);
                    break;
            }
        }

        private static void DrawTabCloser(GraphicsPath closerPath, GraphicsPath closerButtonPath, Graphics graphics, Color closerColor, Color closerFillColor, Color closerOutlineColor) {
            if (closerButtonPath != null) {
                if (closerFillColor != Color.Empty) {
                    graphics.SmoothingMode = SmoothingMode.None;
                    using (Brush closerBrush = new SolidBrush(closerFillColor)) {
                        graphics.FillPath(closerBrush, closerButtonPath);
                    }
                }
                if (closerOutlineColor != Color.Empty) {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Pen closerPen = new Pen(closerOutlineColor)) {
                        graphics.DrawPath(closerPen, closerButtonPath);
                    }
                }
            }
            if (closerColor != Color.Empty) {
                using (Pen closerPen = new Pen(closerColor)) {
                    closerPen.Width = 1;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.DrawPath(closerPen, closerPath);
                }
            }
        }

        protected internal virtual GraphicsPath GetTabCloserButtonPath(Rectangle closerButtonRect) {
            GraphicsPath closerPath = new GraphicsPath();
            closerPath.AddLine(closerButtonRect.X, closerButtonRect.Y, closerButtonRect.Right, closerButtonRect.Y);
            closerPath.AddLine(closerButtonRect.Right, closerButtonRect.Y, closerButtonRect.Right, closerButtonRect.Bottom);
            closerPath.AddLine(closerButtonRect.Right, closerButtonRect.Bottom, closerButtonRect.X, closerButtonRect.Bottom);
            closerPath.AddLine(closerButtonRect.X, closerButtonRect.Bottom, closerButtonRect.X, closerButtonRect.Y);
            closerPath.CloseFigure();
            return closerPath;
        }

        public void DrawTabCloser(Rectangle closerButtonRect, Graphics graphics, TabState state, Point mousePosition) {
            if (!_ShowTabCloser) return;
            using (var closerPath = GetTabCloserPath(closerButtonRect)) {
                using (var closerButtonPath = GetTabCloserButtonPath(closerButtonRect)) {
                    DrawTabCloser(closerPath, closerButtonPath, graphics, state, mousePosition);
                }
            }
        }

       protected internal virtual GraphicsPath GetTabCloserPath(Rectangle closerButtonRect) {
           GraphicsPath closerPath = new GraphicsPath();
           closerPath.AddLine(closerButtonRect.X + TabCloserPathInset, closerButtonRect.Y + TabCloserPathInset, closerButtonRect.Right - TabCloserPathInset, closerButtonRect.Bottom - TabCloserPathInset);
           closerPath.CloseFigure();
           closerPath.AddLine(closerButtonRect.Right - TabCloserPathInset, closerButtonRect.Y + TabCloserPathInset, closerButtonRect.X + TabCloserPathInset, closerButtonRect.Bottom - TabCloserPathInset);
           closerPath.CloseFigure();

           return closerPath;
       }

        public virtual void DrawTabFocusIndicator(GraphicsPath tabpath, TabState state, Graphics graphics) {
            if (_FocusTrack && state == TabState.Focused) {
                Brush focusBrush = null;
                RectangleF pathRect = tabpath.GetBounds();
                Rectangle focusRect = Rectangle.Empty;
                switch (TabControl.Alignment) {
                    case TabAlignment.Top:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, FocusColor, SystemColors.Window, LinearGradientMode.Vertical);
                        break;
                    case TabAlignment.Bottom:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Bottom - 4, (int)pathRect.Width, 4);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, FocusColor, LinearGradientMode.Vertical);
                        break;
                    case TabAlignment.Left:
                        focusRect = new Rectangle((int)pathRect.X, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, FocusColor, SystemColors.ControlLight, LinearGradientMode.Horizontal);
                        break;
                    case TabAlignment.Right:
                        focusRect = new Rectangle((int)pathRect.Right - 4, (int)pathRect.Y, 4, (int)pathRect.Height);
                        focusBrush = new LinearGradientBrush(focusRect, SystemColors.ControlLight, FocusColor, LinearGradientMode.Horizontal);
                        break;
                }

                //	Ensure the focus strip does not go outside the tab
                Region focusRegion = new Region(focusRect);
                focusRegion.Intersect(tabpath);
                graphics.FillRegion(focusBrush, focusRegion);
                focusRegion.Dispose();
                focusBrush.Dispose();
            }
        }

        protected internal virtual void PaintTabBackground(GraphicsPath tabBorder, TabState state, Graphics graphics) {
            using (Brush fillBrush = GetTabBackgroundBrush(state, tabBorder)) {
                //	Paint the background
                graphics.FillPath(fillBrush, tabBorder);
            }
        }

        #endregion
		
		#region Background brushes

		public virtual Brush GetPageBackgroundBrush(TabState state){
            Color color = Color.Empty;

            switch (state) {
                case TabState.Disabled:
                    color = PageBackgroundColorDisabled;
                    break;
                case TabState.Focused:
                    color = PageBackgroundColorFocused;
                    break;
                case TabState.Highlighted:
                    color = PageBackgroundColorHighlighted;
                    break;
                case TabState.Selected:
                    color = PageBackgroundColorSelected;
                    break;
                case TabState.Unselected:
                    color = PageBackgroundColorUnselected;
                    break;
            }
            return new SolidBrush(color);
		}

        protected internal Brush GetTabBackgroundBrush(TabState state, GraphicsPath tabBorder) {
            (Color color1, Color color2) = GetTabBackgroundColors(state);
            return CreateTabBackgroundBrush(color1, color2, state, tabBorder);
        }

        internal virtual (Color color1, Color color2) GetTabBackgroundColors(TabState state) {
            switch (state) {
                case TabState.Disabled:   return (TabColorDisabled1, TabColorDisabled2);
                case TabState.Focused:    return (TabColorFocused1, TabColorFocused2);
                case TabState.Highlighted: return (TabColorHighlighted1, TabColorHighlighted2);
                case TabState.Selected:   return (TabColorSelected1, TabColorSelected2);
                default:                  return (TabColorUnselected1, TabColorUnselected2);
            }
        }

        protected internal virtual Brush CreateTabBackgroundBrush(Color color1, Color color2, TabState state, GraphicsPath tabBorder) {
            LinearGradientBrush fillBrush = null;

            //	Get the correctly aligned gradient
            var tabBounds = tabBorder.GetBounds();
            switch (TabControl.Alignment) {
                case TabAlignment.Top:
                    tabBounds.Height += 1;
                    fillBrush = new LinearGradientBrush(tabBounds, color2, color1, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Bottom:
                    fillBrush = new LinearGradientBrush(tabBounds, color1, color2, LinearGradientMode.Vertical);
                    break;
                case TabAlignment.Left:
                    fillBrush = new LinearGradientBrush(tabBounds, color2, color1, LinearGradientMode.Horizontal);
                    break;
                case TabAlignment.Right:
                    fillBrush = new LinearGradientBrush(tabBounds, color1, color2, LinearGradientMode.Horizontal);
                    break;
            }

            //	Add the blend
            fillBrush.Blend = GetBackgroundBlend();
            return fillBrush;
        }

        protected virtual Blend GetBackgroundBlend() {
            float[] relativeIntensities = { 0f, 0.7f, 1f };
            float[] relativePositions = { 0f, 0.6f, 1f };

            //	Glass look to top aligned tabs
            if (BlendStyle == BlendStyle.Glass) {
                relativeIntensities = new float[] { 0f, 0.5f, 1f, 1f };
                relativePositions = new float[] { 0f, 0.5f, 0.51f, 1f };
            }

            Blend blend = new Blend
            {
                Factors = relativeIntensities,
                Positions = relativePositions
            };

            return blend;
        }

        #endregion
		
		#region Tab border and rect

        public GraphicsPath GetTabBorder(Rectangle tabBounds) {
			
			GraphicsPath path = new GraphicsPath();
			
			AddTabBorder(path, tabBounds);
			
			path.CloseFigure();
			return path;
		}

		#endregion
		
	}
}
