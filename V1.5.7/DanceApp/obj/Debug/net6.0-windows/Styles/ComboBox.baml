   M S B A M L   `   `   `  ����W  SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ComboBoxNormalBorderBrush  �� Color ! ComboBoxNormalBackgroundBrush # ComboBoxDisabledForegroundBrush # ComboBoxDisabledBackgroundBrush  ComboBoxDisabledBorderBrush �� 
TargetType   ComboBoxToggleButtonTemplate � ColumnDefinitions �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName �� Property	 �� Value  ComboBoxFlatStyle
 �� 
TargetType �� 	ClickMode d� 	IsChecked �� Path �� RelativeSource �� Mode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate �� 
IsReadOnly �� 
IsReadOnly � AllowsTransparency �� IsDropDownOpen J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight-   'System.Windows.Input.KeyboardNavigation    DirectionalNavigation�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %�  &	        &	 !     &	 ?     &	 ]     &	 {     &	 �     7   7  7  7  7  7  7  &	     7  7   7  �� 5      $  #348EF6��68   �� 5      $
  #fff��6<   �� 5      $
  #888��6>   �� 5      $
  #eee��6>   �� 5      $
  #888��6:   �� 5
      " d�6   .R�5   
   �  5      �� 5      �� 5      $��20 �6#   .+�5      �� $��Border��6)   $��2��6   8��  $ 5��5      $��
1, 1, 1, 1q�6    8�� 5      �� 5      $��ButtonBorder��6L   $��1��6   $��
1, 1, 1, 1q�6%   ���#444$ 5��5      $��
0, 0, 0, 0q�6    8�� 5      F� 5      $��Arrow��6   $��1��6    =����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=�5      ���#444$��Center=�5      5       5   
   P� 5       �@J�IsMouseOver$
 True��67   . �5      �� $ ButtonBorder��65    � ��
	 �����6O   5       P� 5!       �@d�	IsChecked$
 TrueY�68   . �5"      �� $ ButtonBorder��65    � ��
	 �����6O   �� 5#      $ Arrow��6/    � ��	 �	#FF8D979E6B   5$      P� 5%       � {�$ False��65   . �5&      �� $ Border��65   8	   � ��6I   �� 5'      $ ButtonBorder��65   8	   � ��6O   �� 5(      $ ButtonBorder��67   8	   � ��6Q   �� 5)      8	   � ��6;   �� 5*      $ Arrow��6/    � ��	 �#9996B   5+      5-      �� 5/      "
 ��6'   .�50   
   ��  �@J�SnapsToDevicePixels$
	 True��6:   �� 51   
    �@�OverridesDefaultStyle$
	 True��6C   �� 52   
    � ��$
	 Auto=�6G   �� 53   
    � ��$
	 Auto=�6E   �� 54   
    � ��$
	 True��6:   �� 55   
    � ��
	 �   �63   �� 56   
    �@�FocusVisualStyle	 6>   Z� �� 57   
    � ��$	 Center=�6.   �� 59   
    � ��	 5:      �� 5;      " ��6"   .R�5<      � .+�5=      d� $��ToggleButton��6'   $��2��6;   $ Press=�5>      $|�False��6#   8��   5?      �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=�5@      �� 5B      $��ContentSite��6+   $��5, 3, 23, 3q�6>   $ False��6S   $
��Left=�5C      $��Center=�6:   #��z 5D      #��z 5E      #��z��5F      �� 5G      $��PART_EditableTextBox��6"   $��3, 3, 23, 3q�6>   # z 5H      $y�Hidden=�5I      
������ $
��Left=�5J      $��Center=�61   $
|�True��5K      .��6(   ��5L      �� 5M   "   " ��62   .R�5N   &   �� $��PART_ContentHost��6-   $|�False��6E   5O   #   .��5P   0   5Q      � 5S      $��Popup��6    $��Bottom=�6-   $|�False��5T      $
 True��6&   #��z 5U      $��Slide=�5V      .��5W      � $��DropDown��6#   $
 True��63   #��z 5X      #��z 5Y      .+�5Z   "   �� $��DropDownBorder��6)   
�������$��
0, 1, 0, 0q�6R   $ 0,0,5,5��5[      $��1,1,1,1q�60   8�� 5\      �� 5]   "   $��4q�6/   $
 True��6:   .��5^   &   �� $ 	Contained=�65   5_   #   5`      5a      5b       5c      P� 5d       �@��HasItems$ False��6C   . �5e      �� $ DropDownBorder��6K    � ��$	 95��6g   5f      P� 5g       � {�$ False��6A   . �5h      �� 8	   � ��6G   5i      P� 5j       �@��
IsGrouping$
 True��6E   . �5k      ��  � ��$	 False��6N   5l      P� 5m       �@��
IsEditable$
 True��6A   . �5n      ��  �@  	IsTabStop$	 False��6M   �� 5o      $ PART_EditableTextBox��6E    � y�$	 Visible=�6g   �� 5p      $ ContentSite��6E    � y�$	 Hidden=�6^   5q      5s      5u      5v      5w      