   M S B A M L   `   `   `  ����I  EControlLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:ControlLibrary;assembly=ControlLibraryControlLibrary  5   	    GUI   GUI.WindowQuanLyPhanQuyen   ;my4clr-namespace:ControlLibrary;assembly=ControlLibrary  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  WindowQuanLyPhanQuyen��5   	   $	��768��6'   $
��1024��64   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��5 �6   �� 5	      $
��100* �6   .+�5   
   � $��0��6   $��0��6    � RowDefinitions 5      �� 5      $	��100 �6    �� 5      $	��100 �6    �� 5      $	��100 �6    �� 5      $	��100 �6    .+�5      *   $ControlLibrary.POSButtonIconVertical  -   5        $��btnPhanQuyen��5   I   $��0��6'   $��Phân quyền��64   $��5q�6]      Image$/ )/GUI;component/Images/printer%20green.png��6h      ImageHeight$ 32Q�6�      
ImageWidth$ 32Q�6�   6�     5      -   5        $��btnNhomQuyen��5   T   $��1��6'   $��Nhóm quyền��64   $��5q�6I   $/ )/GUI;component/Images/printer%20green.png��6h   $ 32Q�6�   $ 32Q�6�   6�     5      -   5        $��btnNhomQuyenNhanVien��5   ^   $��2��6'   $��Nhóm quyền nhân viên��64   $��5q�6S   $/ )/GUI;component/Images/printer%20green.png��6z   $ 32Q�6�   $ 32Q�6�   6�   5      �� 5   
   $��1��6   $��Stretch=�6$   $��1��6@   $��2q�6J   ��5      �� 5       �� EndPoint$ 1,0.5&�6&    �� 
StartPoint$ 0,0.5&�67    � MappingMode$ RelativeToBoundingBox=�6J   	 � SpreadMethod$	 Repeat=�6n   .��5      � 
 � Color$
 White��6#    � Offset$ 0Q�61   � 5      $

 Blue��6#   $	 0.1Q�60   � 5      $	
 Red��6#   $	 0.9Q�6/   � 5      $
 White��6#   $ 1Q�61   5      5      �� 5    
   -   5        $��	spNoiDung��5    %   $��2��6   $��Stretch=�66   $��Stretch=�6T   5!      5"      