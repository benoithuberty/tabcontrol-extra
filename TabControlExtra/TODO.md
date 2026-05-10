# Customizeable selected edge line color
# Customizable selected edge line height
# Add new TabStyle providers
	- TabStyleProvider that uses the current theme's color scheme to determine the colors of the tabs. Call it TabStyleThemeAwareProvider
	- TabStyleProvider that uses flashy colors for the tabs, regardless of the current theme. Call it TabStyleFlashyProvider
		- It takes a base color: #4d96ff - and generates a color scheme based on that color, using different shades and tints of the base color for the different states of the tabs (normal, hovered, selected, etc.)
# Customizable font style, color and size for the tab labels

