   M S B A M L   `   `   `  ����C  ?OOP_KURS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:DocCreator
DocCreator  5   	       DocCreator.BankForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:DocCreator     5   	     � Title$  Банк��5   	   $	��174��6   $	��555��6#    � WindowStartupLocation$ CenterScreen=�6/   .��5	      � -   5         �Name$ 
rootWindow��5	      .+�5
   
   �  � RowDefinitions 5      �� 5      6"   �� 5      6"   �� 5      $
��0.5* �6    60    � ColumnDefinitions 5      �� 5      $��1,01* �6#   63   �� 5      $��0,99* �6#   63   .+�5      �� $��0��6   $��0��6"   $*��$Наименование банка:��62   $
��Left=�6P   $��Center=�6l   $��26��6�   $��14(�6�   $
��Bold"�6�   �� 5      $��1��6   $��0��6"   $E��?Банковский идентификационный код:��62   $
��Left=�6^   $��Center=�6z   $��26��6�   $
��Bold"�6�   $��14(�6�   �� 5      $��0��6   $��1��6A   $
��Left=�6Q   $��24��6l    �� TextWrapping$
 Wrap=�6y   $��Center=�6�   $	��250��6�   ��6$   ��  �� Path$
 Name�6�   �� 5      -   5         ��Name$ TextBox_BIK��5   P   $��1��6   $��1��6@   $
��Left=�6f   $��23��6�   $
 Wrap=�6�   $��Center=�6�    �� 	MaxLength$ 9��6�   $��70��6�   ��6$   �� $	 BIK�6�   �� 5      -   5        $��Добавить��5      $��1��6(   $��2��68   $��Right=�6F   $��Center=�6c   $��75��6~   $��14(�6�   $��0,3,3,3q�6�   5       5!      5"      