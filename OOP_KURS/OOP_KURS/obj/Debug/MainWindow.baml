   M S B A M L   `   `   `  ����C  ?OOP_KURS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:DocCreator
DocCreator  5   	       DocCreator.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:DocCreator     5   	   -   5          �Name$
  Main��5   _    � Title$ Главная��6	   $��704.251��6   $��1037.87��6*    � WindowStartupLocation$ CenterScreen=�6:    � 	Resources 5	         MyViewSource  	StyleTest=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� BasedOn �� 
TargetType �� Property �� Value��%�   &	        &	 %     7 �� 5
   
   -   5        5      �� 5   
   8   "  6\   .�5      ��  � ��
 �����6?   �� 5       � ��
 �����6;   �� 5       � ��$ 10,0,0,0q�6(   6;   5      5      .��5      � .+�5   
   o� $��25��6   $	��Top=�6   .@�5      m� $��Документ��6   .@�5      m� -   5        $��Создание��5      6H   5      m� 5      $��Справочники��6   .@�5      m� -   5         m�Name$ Btn_Customer��5      $��Заказчики��61   6b   m� 5      -   5        $ Btn_ProductAndService��5      $��Товары/услуги��6V   6o   �� 5      m� 5      -   5        $ Btn_Bank��5      $��
Банки��6-   6[   5      m� 5       -   5        $��Реквизиты��5       5"      5#      �� 5%   
   -   5        	 ��Name$		 Doc��5%      $��Документы: ��6   $
��Left=�63   $��	29,45,0,0q�6N   $	��Top=�6a   �� 5'   
   -	   5        
 ��Name$
 	Check_Acc��5'       d� 	IsChecked$
 TrueY�6&   $��
Счета��67   $
��Left=�6G   $��
184,51,0,0q�6b   $	��Top=�6v   6�   �� 5(   
   -
   5        $
 	Check_Act��5(      $
 TrueY�6&   $��Акты��67   $
��Left=�6F   $��
262,51,0,0q�6a   $	��Top=�6u    J� RenderTransformOrigin$ 	0.076,0.6&�6�   6�   � 5*   
   -   5         �Name$ PopupFilter��5*   !    � 	StaysOpen . $��Bottom=�67   ��.  .��5+      �� 
���   �$��1q�6)   ._�5,      � 
�������$	��161��6*   $	��200��66   .+�5-      �� -   5         ��Name$ 	ListBox_1��5-      ��61   �� ��5.      �� 5/      .R�50   "   �� -   5        ��50   +   ��  �� Path$
 Text� 6I   �� $ 
IsSelected�6o   51      53      54      55      56      &   System.Windows.Controls.DataGrid  58   
   -   5          Name$ DG��58         CanUserAddRows .     AutoGenerateColumns .  $
��Left=�6�   $	��568��6�   $��	29,76,0,0q�6�   $	��Top=�6�   $	��968��6�    � Language$ ru-RU�6M  ��6   ��  �� Source# [  59      5;      5<      