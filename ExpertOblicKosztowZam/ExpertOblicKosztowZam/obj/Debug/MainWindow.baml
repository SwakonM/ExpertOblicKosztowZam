   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   6    ExpertOblicKosztowZam&    ExpertOblicKosztowZam.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35T PScottPlot.WPF, Version=4.1.3.0, Culture=neutral, PublicKeyToken=e53b06131e34a3aaR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Nribbon9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      5my&clr-namespace:System;assembly=mscorlib     5   6   -   5        1  +System.Windows.Controls.Ribbon.RibbonWindow   Name$  RibbonWindow��5       � Title$ ExpertOblicKosztowZam��5   +   $
��1280��5      $
��1024��6    � WindowState$ 	Maximized=�6    � 	Resources 5	       
  Width1  Height1��%J   &	        &	      X�5   
   NaNQ�6(   X�5   
   NaNQ�6)   5      .��5      � -   5         �Name$ 
LayoutRoot��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��5 �6   .+�5   
   +  %System.Windows.Controls.Ribbon.Ribbon  -   5          Name$ Ribbon��5      .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  -   5          Name$ rtParams��5      $��Params��61   $��0q�6A   .@�5      0  *System.Windows.Controls.Ribbon.RibbonGroup  -   5          Name$ Group1��5   %   $��
Inicjalize��65   .@�5      1  +System.Windows.Controls.Ribbon.RibbonButton  -   5        	  Name$	 btRead��5   *   
   Label$
 Wczytaj��6:      LargeImageSource$ Image/applications.png��6J   6t     5      -   5        $	 
btOpenGrid��5   *   $
 Zamówienia��6>   $ Image/make.png��6Q   6s     5      -   5        $	 btCalculate��5   *   $
 Strukura Procesu��6?   $% Image/calculator_operations.png��6X   6�     5      -	   5        $	 
btSchedule��5   *   $
 Harmonogram procesu��6>   $ Image/kviewshell.png��6Z   6�     5      -
   5        $	 btOrders��5   *   $
 Harmonogram zamówień��6<   $ lists_64x64.png��6Y   6|     5      -   5        $	 btExit��5   *   $

 Exit��6:   $ Image/exit1.png��6G   6j   5      5      5       �� 5"   
   -   5        $��tcMdi��5"      $y�Visible=�6"   $��Stretch=�67   $��Stretch=�6Z   
�������$��5q�6�   $
��Auto=�6�   $
��Auto=�6�   $��1��63  #��[  6@  #��[ 6`  ��5#      �� 5$       �� EndPoint$ 1,0.5&�6&    �� 
StartPoint$ 0,0.5&�67   .��5%      �  � Color$ 	#FFECF5FC��6#    � Offset$ 0Q�65   � 5&      $ 	#FFD0D7E5��6#   $ 1Q�65   5'      5)      5*      5-      