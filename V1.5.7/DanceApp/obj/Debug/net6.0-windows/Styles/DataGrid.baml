   M S B A M L   `   `   `  ����b  ^PresentationFramework.Aero2, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35hKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2Microsoft.Windows.Themes  5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   DataGridStyle1  �� 
TargetType&    System.Windows.Controls.DataGrid �� Property �� Value5  /System.Windows.Controls.DataGridLengthConverter �� 
TargetType �� Padding J� SnapsToDevicePixels ��Name � ColumnDefinitions � RowDefinitions	    SelectAllCommand
 �� 
ResourceId �� TypeInTargetAssembly!    HeadersVisibilityConverter �� 	Converter7  1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter �� RelativeSource �� AncestorTypeG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter  Name   Column ��Name �� CanContentScroll ��Name �� ScrollableHeight � Maximum �� Orientation �� ViewportHeight �� ViewportSize* �� #ComputedVerticalScrollBarVisibility � Value �� Mode �� ScrollableWidth  �� ViewportWidth,! �� %ComputedHorizontalScrollBarVisibility" �� Triggers# ]� 
Conditions$ �� Property% �� Value  DataGridRowStyle1)  #System.Windows.Controls.DataGridRow& ��Name?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid?  9System.Windows.Controls.Primitives.DataGridCellsPresenter'   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter(   DetailsVisibility$)   SelectiveScrollingOrientation#*    RowDetailsScrollingConverter;  5System.Windows.Controls.SelectiveScrollingOrientation+   Vertical:	  4System.Windows.Controls.Primitives.DataGridRowHeader
,   Row- P� Property. P� Value  DataGridCellStyle1*
  $System.Windows.Controls.DataGridCell 
 DGCell  DGCellAddEditDelete  ColumnHeaderGripperStyle  DataGridColumnHeaderStyle1=  7System.Windows.Controls.Primitives.DataGridColumnHeader3   -Microsoft.Windows.Themes.DataGridHeaderBorder/   CanUserSort0   IsClickable1   	IsPressed2 J� IsMouseOver3   	IsHovered4   SeparatorBrush5   SeparatorBrush6   SortDirection7   SortDirection8   SeparatorVisibility9   SeparatorVisibility: n�Name  DGridCentAl�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      UthemeKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2  5      %�  &	        &	 �
    &	 �    &	 B    &	 I    &	 '    &	 9    7  7  &	 F    �� 5      "    6#   .�5   
   ��  � ��
 ���� 6'   �� 5   
    � ��
 �   �6'   �� 5   
    � ��$ 2q�6,   �� 5	   
     �@  RowDetailsVisibilityMode$ VisibleWhenSelected=�65   �� 5
   
    � ��$
 true��6:   �� 5   
    �@��PanningMode$
 Both=�65   �� 5   
    �@��IsFlicksEnabled$ False��63   �� 5   
    �@  AutoGenerateColumns$ False��60   �� 5   
    �@  HeadersVisibility$ Column=�6.   �� 5   
    � ��$ 5��6%   �� 5   
    �@  SelectionMode$ Single=�6*   �� 5   
    �@  SelectionUnit$ FullRow=�6*   �� 5   
    �@  CanUserAddRows$ False��6+   �� 5   
    �@  CanUserDeleteRows$ False��6.   �� 5   
    �@  CanUserReorderColumns$ False��62   �� 5   
    �@  CanUserResizeRows$ False��6.   �� 5   
    � ��$ 13(�6%   �� 5   
    �@  GridLinesVisibility$
 None=�60   �� 5   
    �@  ColumnWidth$ * 6(   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� #��z��6   #��z��6G   #��z��6{   # z��6�   $
 True��6�   ._�5      �� $ DG_ScrollViewer��6'   $|�false��6@   ��5      �� 5    "   " ��62   .R�5!   &   �  5"   *   �� 5#   .   $
��Auto �6?   �� 5$   .   $��* �6?   �� 5%   .   $
��Auto �6?    5'   *   �� 5(   .   $
��Auto �6<   �� 5)   .   $��* �6<   �� 5*   .   $
��Auto �6<   .+�5,   *   �� #��Z	 61   $|�false��6`   ��6r   C� *�� $"
 DataGridSelectAllButtonStyle��"   +y�6�   �� *HeadersVisibility+# Z # Z  �� "   ��6�  �� *CellsPanelHorizontalOffset+ �� "     5-   *   $! PART_ColumnHeadersPresenter��6I   $��1��6n   y�6~   �� *HeadersVisibility+# Z # Z  �� "   �� 5.   *   $! PART_ScrollContentPresenter��6A   $��2��6f   # z��6z   $��1��6�   �� 5/   *   $ PART_VerticalScrollBar��64   $��2��6T   # z 6d   $ Vertical=�6�   $��1��6�   # z 6�   #y�z 6D   6�   �� *VerticalOffset+$ OneWay=� �� *TemplatedParent+6D  � 50   *   $��1��6/   $��2��6?    51   .   �� 52   2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "   �� 53   2   $��* �6C   .+�55   .   �� $ PART_HorizontalScrollBar��68   $��1��6Z   # z 6j   $ 
Horizontal=�6�   # z  6�   #y�z! 6?   6�   �� *HorizontalOffset+$ OneWay=� �� *TemplatedParent+6?  56   +   57   '   58   #   .��5:      �� # z 6-   5;      5<      5=      5?      " 5A   
   ]� 5B      # 5C      �� 5D      $ �@��
IsGrouping$
% true��66   �� 5E      "$ �@%�IsVirtualizingWhenGrouping$% false��6X   ./�5G      ��  � ��$ false��6B   5H      5J      �� 5L      "   6&   .�5M   
   ��  � ��$ 0 5 -1 0q�6#   �� 5N   
    � �� �#FFFFFF6'   �� 5O   
    �@J�SnapsToDevicePixels$
 true��60   �� 5P   
    �@=�ErrorTemplate 65   Z� �� 5Q   
    �@ ValidationErrorTemplate 5R      �� 5S      .R�5T      �� 
���  ��$��!��61   $��Center=�6:   5U      5W      �� 5Y   
    � �� 5Z      �� 5[      "  6"   .R�5\      �� $& 
DGR_Border��6   $	 0 4q�61   #��z��6?   $
 True��6i   ._�5]         5_      �� 5`   "   $
��Auto �63   �� 5a   "   $��* �63    5d      �� 5e   "   $��* �60   �� 5f   "   $
��Auto �60   .+�5i        $��1��65   #��z' 6E   # z 6o     5j      $��1��67   $��1��6G   #y�z( 5l   b   ) 5j   T   �� *AreRowDetailsFrozen+# Z* # Z+  �� "   5l   b   	  5n      $��2��60   $) Vertical=�6A   y�6�   �� *HeadersVisibility+# Z # Z,  �� "   5r      5s      5t      5v      " 5x   
   P� 5y      - �@J�IsMouseOver$
. True��6-   . �5z      ��  � �� �	#FF0A8FC86/   �� 5{       � �� �#ffffff6/   5|      P� 5}      - �@ 
IsSelected$
. True��6,   . �5~      ��  � �� �	#FF0A8FC86/   �� 5       � �� �#ffffff6/   5�      ]� 5�      # 5�      �� 5�      $ �@ 
IsSelected$
% true��66   �� 5�      $ �@��IsSelectionActive$% false��6F   ./�5�      ��  � �� �#dfdfdf6/   5�      5�      �� 5�      "  
 6'   .�5�   
   ��  � ��
 ���� 6'   �� 5�   
    � ��$ Center=�6.   �� 5�   
    �@�FocusVisualStyle 6-   Z� �� 5�   
    � ��$ 5 0 0 0q�6#   �� 5�   
    � �� 5�      �� 5�      " 
 6"   .R�5�      �� #��z��6   $��0q�6G   $
 True��6[   ._�5�      �� # z 6+   5�      5�      5�      5�      �� 5�      "  
 6   .�5�   
   ��  � ��$ 2 0 0 0q�6#   �� 5�   
    � �� 5�      �� 5�      " 
 6"   .R�5�      � #��z��6   .+�5�      �� $��Center=�6+   5�      5�      5�      �� 5�   
    � ��$ 14(�6%   5�      �� 5�      "  
 6(   .�5�   
   ��  � �� 5�      �� 5�      " 
 6"   .R�5�      � #��z��6   .+�5�      �� $��Center=�6+   5�      5�      5�      �� 5�   
    � ��$ 14(�6%   5�      �� 5�      "  n�6-   .�5�   
   ��  � ��$ 8��6"   �� 5�   
    � ��
 ���� 6'   �� 5�   
    �@�Cursor$ SizeWE��6#   �� 5�   
    � �� 5�      �� 5�      " n�6"   .R�5�      �� #��z��6   # z��6G   5�      5�      5�      �� 5�      "   6/   .�5�   
   ��  � ��$ Center=�65   �� 5�   
    � �� �	#FF0A8FC86'   �� 5�   
    � �� �#FDFEFE6'   �� 5�   
    � ��$ 34��6#   �� 5�   
    � ��$ 0,0,1,0q�6,   �� 5�   
    � ��
 �����6(   �� 5�   
    � ��$ 16(�6%   �� 5�   
    � �� 5�      �� 5�      "  6"   .R�5�      � .+�5�        #��z��65   #��z��6_   #��z��6�   #0 z/ 6�   #1 z��6�   #3 z2 6  # z��6=  #5 z4 6a  #7 z6 6�  #9 z8 6�  ._�5�      �� #��z��6/   $
��True��6r   # z 6�   #��z��6�   5�      n� 5�      $: PART_LeftHeaderGripper��6    $
��Left=�6@   8��  6[   n� 5�      $: PART_RightHeaderGripper��6    $��Right=�6A   8�� 6]   5�      5�      5�      5�      �� 5�      "   6    .�5�   
   ��  � ��$ Center=�6=   �� 5�   
    � �� �#348EF66'   �� 5�   
    � �� �#FDFEFE6'   �� 5�   
    � ��$ 24��6#   �� 5�   
    � ��$ 0,0,1,0q�6,   �� 5�   
    � ��
 �   �6(   �� 5�   
    � ��$ 18(�6%   5�      5�      