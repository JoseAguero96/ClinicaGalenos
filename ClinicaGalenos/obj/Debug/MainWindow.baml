   M S B A M L   `   `   `  ����I  EClinicaGalenos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ClinicaGalenosClinicaGalenos  5   	       ClinicaGalenos.MainWindow   5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5	   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5
   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ClinicaGalenos     5   	      MaterialDesignBody#���   5   	     MaterialDesignPaper#���  5   	   $��Medium"�5   	   $��14(�5   	   $X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5   	     � Title$  
MainWindow��5   	   $	��300��6   $	��425��6)    � WindowStyle$
 None=�65    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�6b   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      $	��100 �6   6+   �� 5      $��* �6   6)   .+�5   
   �� -   5        $��0��5      
������ ._�5      � .+�5      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ PrimaryDark=�6+   ��6>   Z� .��5      �� $��Right=�6!   .+�5      �� -   5         ��Name$ 	btnCerrar��5   !   $��30��64   $��30��6?   $��0q�6K   
������ ��6p   Z� ��6�   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ WindowClose=�66   ���	#FF060000$��20��6`   $��20��6k   5      5      5      5       5!      �� 5"   
   $��1��6   
������  �� Padding$ 0q�67   ._�5#      � .+�5$        $ PrimaryDark=�6+   ��6>   Z� .��5%      �� $	��283��6   $��73��6(   $��img/galenoslogo.png��64   $
��71,0q�6Q   6b   5&      5'      5(      �� 5)   
   -   5        	 ��Name$	 label��5)      $��Ingresar al Sistema��6   $
��Left=�6=   $��
127,56,0,0q�6X   $��1��6l   $	��Top=�6y   $��20(�6�   �� 5+   
   -   5        
 ��Name$
 iconUser��5+      $
��Left=�6"   $��20��6=   $��	40,18,0,0q�6I   $��2��6\   $	��Top=�6i   $��20��6�   $��img/icon_user.png��6�   �� 5,   
   -   5        $
 iconPassword��5,      $
��Left=�6&   $��20��6A   $��	40,67,0,0q�6M   $��2��6`   $	��Top=�6m   $��20��6�   $��img/icon_pass.png��6�   �� 5-   
   -   5         ��Name$ txtRut��5-      $
��Left=�6"   $��37��6=   $��71,3,0,0q�6I   $��2��6[    �� TextWrapping$
 Wrap=�6h   $	��Top=�6|   $	��311��6�    �� SelectionBrush �	#FF006DC36�   G� 5.   
   -   5         G�Name$ txtPass��5.      $
��Left=�6'   $��	71,65,0,0q�6B   $��2��6U   $	��Top=�6b   $	��311��6z   �� 5/   
   -   5        $ btnIngresar��5/      $��Ingresar��6&   $
��Left=�69   $��
40,112,0,0q�6T   $��2��6h   $	��Top=�6u   $	��344��6�   $��39��6�   6�   50      51      