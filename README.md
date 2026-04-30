## TabControlExtra
### An improved TabControl for Windows Forms (.net Framework 4.8, .net 8 and .net 10)

The System.Windows.Forms TabControl provided by Microsoft in the .Net Library 
has a number of limitations that make it unpleasant to use.

This project grew out of an excellent effort by Mark Jackson on CodeProject to 
improve on the Microsoft TabControl. It was named CustomTabControl, and the 
original can be found here: 

[Painting Your Own Tabs - Second Edition](https://www.codeproject.com/Articles/91387/Painting-Your-Own-Tabs-Second-Edition)

This link includes a good description of the failings of Microsoft's TabControl,
and describes in detail the approach taken by Mark to improve it.

In 2011, Richard L King, took over Mark's code to continue the quest of having a customizable tab control for Windows forms and created a git repository for it.
[Github tabcontrol-extra](https://github.com/tradewright/tabcontrol-extra)

As the CPOL license allows it, I've taken Richard's code over to migrate it to .net 10, keeping it backward compatible with .net framework 4.8. 

I've also updated the code to fix DPI issues.


### Licence

This project is licensed under the Code Project Open License (CPOL) Version 1.02
(see the [License.md](https://github.com/tradewright/tabcontrol-extra/blob/master/License.md) file or go [here](https://www.codeproject.com/info/cpol10.aspx)).
