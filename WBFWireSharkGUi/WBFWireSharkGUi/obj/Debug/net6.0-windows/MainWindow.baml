   M S B A M L   `   `   `  ����J  FWBFWireSharkGUi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WBFWireSharkGUiWBFWireSharkGUi  5   	        WBFWireSharkGUi.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WBFWireSharkGUi     5   	     � Title$  
MainWindow��5   	   $	��753��6   $
��1289��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0,0,10,0q�6   .+�5      �� $��1��6   $��Center=�6)   $��Center=�6F   .+�5      �� -   5         ��Name$ DeviceComboBox��5      $	��200��63   $��30��6?   $��0,0,0,20q�6K   �� 5      -   5        $��Start Capture��5      61   �� 5      -   5         ��Name$ PacketInfoTextBox��5      $	��200��65   $	��400��6B    �� 
IsReadOnly$
 True��6N   �� 5      -   5        $��Stop Capture��5      60   5      5      � 5   
   -   5         �Name$ MainGrid��5      5      5       5!      