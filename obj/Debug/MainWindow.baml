   M S B A M L   `   `   `  ����D  @Signature, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Signature	Signature  5   	       Signature.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Signature     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)    � 
ResizeMode$ NoResize=�65   .��5	      �  � ColumnDefinitions 5   
   �� 5      $	��400 �6   �� 5      .+�5   
   �� ���#00632E$
��Left=�6#   $	��112��6>   $	��Top=�6K   $	��794��6c   $��2��6o   �� 5   
   $
��Left=�6   $	��125��6+   $��92��67   $	��Top=�6C   $��	35,10,0,0q�6[   $U��Ohttp://www.boscdelacoma.cat/wp-content/uploads/2016/11/logo_web_transparent.png��6n   �� 5   
   $l��fCFGS DAM
MP09 PROGRAMACIÓ DE SERVEIS I PROCESSOS
UF01 SEGURETAT I CRIPTOGRAFIA
SIGNATURA ELECTRÒNICA��6   $��
200,5,58,0q�6�   $	��Top=�6�   $��Arial)�6�   $	��102��6�   $
��Bold"�6�   ���#B2CFC0$��2��6�   $��20(�6  �� 5   
   -   5        $��imgFoto��5      $��Uniform=�6   $��10,122,10,100q�61   �� 5   
   -   5        $��
btnLoadImg��5      $��20��6#   $
��Left=�6/   $��	10,0,0,70q�6J   $��Bottom=�6]   $	��100��6x   $��Obrir Imatge��6�   �� 5   
   -   5        $��btnSign��5      $��SIGNAR IMATGE��6    $
��Left=�68   $��
100,0,0,10q�6S   $��Bottom=�6g   $	��200��6�   $��50��6�   6�   �� 5   
   -   5        $��txtSignature��5      $��10,122,10,100q�6&    �� TextWrapping$
 Wrap=�6=    �� 
IsReadOnly . $��Arial)�6c   $��1��6v   $��16(�6�   �� 5   
   -   5         ��Name$ btnLoadSignature��5      $��20��6+   $
��Left=�67   $��	10,0,0,70q�6R   $��Bottom=�6e   $	��100��6�   $��Obrir Signatura��6�   $��1��6�   �� 5   
   -   5        $��btnCheck��5      $��COMPROVAR SIGNATURA��6!   $
��Left=�6?   $��
100,0,0,10q�6Z   $��Bottom=�6n   $	��200��6�   $��50��6�   $��1��6�   6�   5      5      