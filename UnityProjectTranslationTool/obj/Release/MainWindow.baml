   M S B A M L   `   `   `  ���� V  RUnityProjectTranslationTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:UnityProjectTranslationToolUnityProjectTranslationTool  [2clr-namespace:UnityProjectTranslationTool.FileData$UnityProjectTranslationTool.FileData  ,    &UnityProjectTranslationTool.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ;local)clr-namespace:UnityProjectTranslationTool     Cfile2clr-namespace:UnityProjectTranslationTool.FileData       � Title$$  Unity Project Translation Tool��$	��450��$	��800��.��]� .+�o� $	��Top=�.@�m� $��_File��.@�m� -   $��Open Translation Project��m� -    m�Name$ 	Menu_Save��$
��Save��m� -   $ Menu_SaveAs��$��Save as��m� -   $ 
Menu_Apply��$��Apply Translation���� m� -   $��Open Source Code Folder��m� $��_Setting��.@�m� $��	Encodings��m� $��	Languages��m� $��Filters��m� $��_About��.@�m� $��Documentation��]� $��10q�.+�R� -    R�Name$ Files��$
��Left=�$
��Auto��$	��140��$	��200��$��Stretch=� � 	Resources  �� DataType5  /UnityProjectTranslationTool.FileData.FolderData �� ItemsSource9  3UnityProjectTranslationTool.FileData.SingleFileData��%{   (��       *, +)(�� 7     *, +)�� "   �� *files+.R��� ���� *name+�� "  .R��� ���� *name+&   System.Windows.Controls.DataGrid  -     Name$ TextEntryGrid��   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  -   column_line��,  &System.Windows.Controls.DataGridColumn	   Header$
	 Line��
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 50    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *line+  -	   column_index��$	 Index��$
 50  .  �� *index+  -
   column_text��$
	 Text��$

 Auto    MinWidth$	 200Q� .  �� *text+  -   column_translation��$	 Translation��$

 Auto $	 200Q� .   �� *translation+