   M S B A M L   `   `   `  ����P  LExpertOblicKosztowZam, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:ExpertOblicKosztowZamExpertOblicKosztowZam  5      %    ExpertOblicKosztowZam.ucSetting   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35T PScottPlot.WPF, Version=4.1.3.0, Culture=neutral, PublicKeyToken=e53b06131e34a3aaR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:ExpertOblicKosztowZam     5      -   5          � 	Resources  5	         HeaderBrush �� 
StartPoint �� EndPoint � Color � Offset  HeaderBorderBrush  HeaderStyle �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value  ItemCollectionViewSource �� CollectionViewType��%  &	        &	 l    &	 @    7   7  &	 �    �� 5   
   $ 0.5,0&�62   $ 0.5,1&�6E   .��5      � $ 	#FF6B8E95��6   $ 0Q�6-   � 5      $ 	#FF14A7C1��6   $ 1Q�6-   � 5      $ 	#FF1E424E��6   $ 0.509Q�6-   � 5      $ 	#FF1D4855��6   $ 0.542Q�6-   � 5      $ 	#FF1D4855��6   $ 0.542Q�6-   � 5      $ 	#FF193A44��6   $ 0.526Q�6-   5      �� 5   
   $ 0.5,0&�68   $ 0.5,1&�6K   .��5      � $ 	#FF1D1D1D��6   $ 0.614Q�6-   � 5      $ 	#FF007F96��6   $ 0.853Q�6-   � 5      $ 	#FF0AEAFA��6   $ 1Q�6-   5      �� 5   
   "  6$   .�5      ��  � ��8   6+   �� 5       � ��
 �����6+   �� 5       � ��8  6,   �� 5       � ��$ 1q�60   �� 5       �@J�SnapsToDevicePixels . 64   �� 5       � ��$ Center=�6;   �� 5        � ��$ 0��6)   �� 5!       � ��$ 50��6*   �� 5"       �@�Cursor$
 Hand��6'   5#      �� 5%   
   " ��6A   5&      .��5'      � 	 � ColumnDefinitions	 5(   
   �� 5)      $	��200 �6   �� 5*      $	��100 �6   �� 5+      $��* �6   
 � RowDefinitions
 5-   
   �� 5.      $��30 �6   �� 5/      $��30 �6   �� 50      $��30 �6   �� 52      $��* �6   �� 53      $��30 �6   .+�55   
   �� $*��$Bazowy współczynnik ilościowy ξ=��6   $��0��6<   �� 56   
   -   5         ��Name$ BazowyWspoczIlos��56      $��0��6,   $��1��69   $��1��6J   6S   �� 57   
   $%��Denominator minimalizujący ζ=��6   $��1��69   �� 58   
   -   5        $ DenominatorMin��58      $��1��6*   $��1��67   $
��1000��6H   6T   �� 59   
   $��Maksymalna wartość =��6   $��2��60   �� 5:   
   -   5        $ 
MaxWartosc��5:      $��2��6&   $��1��63   $	��1.3��6D   &   System.Windows.Controls.DataGrid  5;   
   -   5          Name$ DatagridMovie��5;      $��3��6*   $��3��67      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .    
IsReadOnly .    ColumnHeaderStyle# [ 5<   #   6U   5B      5C      