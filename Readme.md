<!-- default file list -->
*Files to look at*:

* [Converters.cs](./CS/SearchResultViewDemo/Converters.cs) (VB: [Converters.vb](./VB/SearchResultViewDemo/Converters.vb))
* [CustomObjectsProvider.cs](./CS/SearchResultViewDemo/CustomObjectsProvider.cs) (VB: [CustomObjectsProvider.vb](./VB/SearchResultViewDemo/CustomObjectsProvider.vb))
* **[MainWindow.xaml](./CS/SearchResultViewDemo/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/SearchResultViewDemo/MainWindow.xaml))**
* [SearchResultView.xaml](./CS/SearchResultViewDemo/SearchResultView.xaml) (VB: [SearchResultView.xaml](./VB/SearchResultViewDemo/SearchResultView.xaml))
* [SearchResultView.xaml.cs](./CS/SearchResultViewDemo/SearchResultView.xaml.cs) (VB: [SearchResultView.xaml.vb](./VB/SearchResultViewDemo/SearchResultView.xaml.vb))
<!-- default file list end -->
# How to show the appointments search result in SearchResult View for SchedulerControl (similarly to ListView in Outlook)


<p>Microsoft Outlook allows search for appointments in the calendar by their <strong>Subject </strong>and show the search result in a separate grid view. This view also provides the capability to open the Appointment Edit form by the row double-click action and edit the current appointment.<br><br>This example uses the FlipView to switch between the main scheduler view and the search view. To implement a similar functionality, it's possible to use SearchControl managing the grid filter. Use the <strong>Ctrl+E</strong> shortcut to focus SeacrhControl and start typing to switch the scheduler view to the search result view containing filtered data. To return to the main view, clear the search string or use the FlipView button for this purpose. </p>

<br/>


