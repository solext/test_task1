   M S B A M L   `   `   `  ����C  ?TestTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:TestTask.ViewModelTestTask.ViewModel  5   	   #clr-namespace:TestTaskTestTask  5   	   #    TestTask.ViewModel.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /vm clr-namespace:TestTask.ViewModel     5   	   (localclr-namespace:TestTask     5   	     � Title$  TestTask��5	   	    � 	Resources 5
         ButtonStyle1 �� 
TargetType �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray �� 
TargetType � RowDefinitions	 �� EndPoint
 �� 
StartPoint � Color � Offset �� ImageSource ��Name �� TextAlignment��%w	  &	        �� 5   
   " ��6%   .�5      ��  �@�FocusVisualStyle 5      �� 5      .�5      ��  � �� 5      �� 5   "   .R�5   &   �� $��2q�60    . #��� ��6V   $��1��6�   $	 1 2R�6�   5   #   5      5      5      �� 5       � �� �	#FFDDDDDD6+   �� 5       � �� �	#FF7070706,   �� 5       � ��# � ��6+   �� 5       � ��$ 1q�60   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$ 1q�6(   �� 5        � �� 5!      �� 5"      " ��6&   .R�5#      � $
��Auto��6   $��	-5,0,0,-1q�6,   $��Stretch=�6?    5$      �� 5%   "   $��5* �60   �� 5&   "   $	��29* �60   �� 5'   "   $	��11* �60   .+�5)      �� $��Right=�6(   $��50��6D   $��3��6O   $��3��6c   ��5*   "   �� 5+   &   $	 0.5,1&�6:   $
 0.5,0&�6K   .��5,   *   � $ 	#FFDFDFEE��67   $ 0.243Q�6I   � 5-   *   $ 	#FFA9A9FF��67   $
 0.97Q�6I   � 5.   *   $ 	#FFD9D9F9��67   $ 0.591Q�6I   5/   '   52      A� 53      $
��Left=�6&   $��35��6A   $��3��6L   $��3��6`   $��5,0,0,0q�6q   ��54   "   �� 55   &   $	 0.5,1&�6:   $
 0.5,0&�6K   .��56   *   � $ 	#FFDFDFEE��67   $ 0.243Q�6I   � 57   *   $ 	#FFA9A9FF��67   $
 0.97Q�6I   � 58   *   $ 	#FFD9D9F9��67   $ 0.591Q�6I   59   '   5;      �� 5<      $��	25,0,50,0q�6(   $��3��6;   ��5=   "   �� 5>   &   $	 0.5,1&�6:   $
 0.5,0&�6K   .��5?   *   � $ 	#FFDFDFEE��67   $ 0.243Q�6I   � 5@   *   $ 	#FFA9A9FF��67   $
 0.97Q�6I   � 5A   *   $ 	#FFD9D9F9��67   $ 0.591Q�6I   5B   '   5D      A� 5E      $
��Left=�6&   $��30��6A   $��10,-1.001,0,2.111q�6L   $��30��6g   $��2��6s   $��1��6�   ��5F   "   �� 5G   &   $% /TestTask;component/Image/1.png��61   5I      �� 5J      $ 	textBlock��6(   $��52,-0.199,0,2q�6;   $
��Wrap=�6R   $��	Stop Scan��6f   $��24(�6w   $
��Left=�6�   $	��110��6�   $
��Auto��6�   $��Stretch=�6�   $��3��6�   �� 5K      $ 
textBlock1��6(   $��Right=�6<   $
��Wrap=�6X   $��43��6l   $ Center=�6w   $��35��6�   $��Bottom=�6�   $��29.333(�6�   ���	#FFC1BCBC$��0,-0.319,0,0.319q�6�   $��3��6�   $��0��6	  �� 6  $
��Bold"�6  $��Times New Roman)�6.  .�6L  �� $�»��6P  6\  6c  5L      5M      5O      5P      5Q      .��5V      � .+�5W   
   &   System.Windows.Controls.DataGrid     CanUserAddRows .     AutoGenerateColumns .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserSortColumns .     CanUserResizeRows .     RowDetailsVisibilityMode$ 	Collapsed=�5Y   H   $��0,0,0,55q�6m   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6      CanUserResizeColumns .  !   EnableColumnVirtualization .  � UseLayoutRounding .    AreRowDetailsFrozen . ��5W   *   �� *Files+   Columns 5[      4  .System.Windows.Controls.DataGridTemplateColumn  5\         CellTemplate 5]      �� 5^      .R�5_      ��  ��Name$ checkbox��6'    d� 	IsChecked 69   ��   �� Path$  ischeck�! �� Mode$! TwoWay=�" �� UpdateSourceTrigger$" PropertyChanged=���6�   �� *	chekall+$! TwoWay=���6�   �� 6�   5`      5b        5c      ,  &System.Windows.Controls.DataGridColumn#   Header$
# Icon��6)    5d      �� 5e      .R�5f      �� ��6$   �� $
  Icon�$" PropertyChanged=�6h   5g      5i      0  *System.Windows.Controls.DataGridTextColumn  5j      $
# Name��6%   1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ 63   �� $
  Name�$" PropertyChanged=�  5k      $# 	Extension��6%   $ 68   �� $  	Extension�$" PropertyChanged=�  5l      $# 	Only path��6%   $ 68   �� $
  Path�$" PropertyChanged=�  5m      $# Created date��6%   $ 6;   �� $  DateCreated�$" PropertyChanged=�  5n      $# Modified date��6%   $ 6<   �� $  DateModified�$" PropertyChanged=�  5o      $# Digital Signature��6%   $ 6@   �� $  	Signature�$" PropertyChanged=�  5p      $
# Hash��6%   $ 63   �� $
  Hash�$" PropertyChanged=�5r      �� 5s   
   -   5        % ��Name$% checkbutton��5s      $��	Check All��6&   $��	0,0,20,18q�6�   $��20��6�   $��Bottom=�6�   $��Right=�6�   $	��184��6�   ��6T   �� *	chekall+$! OneWay=���6}   �� 6�   5u      5v      