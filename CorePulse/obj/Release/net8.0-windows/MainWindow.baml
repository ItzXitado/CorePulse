   M S B A M L   `   `   `  ���� D  @Emoji.Wpf, Version=0.3.4.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:Emoji.Wpf;assembly=Emoji.Wpf	Emoji.Wpf  E AFluentWPF, Version=0.10.2.0, Culture=neutral, PublicKeyToken=nullP6clr-namespace:SourceChord.FluentWPF;assembly=FluentWPFSourceChord.FluentWPF D @CorePulse, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CorePulse	CorePulse    CorePulse.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     S OHandyControl, Version=3.5.2.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  6emoji*clr-namespace:Emoji.Wpf;assembly=Emoji.Wpf  +handyhttp://schemas.handyorg.com/wpf  ?fw6clr-namespace:SourceChord.FluentWPF;assembly=FluentWPF  R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:CorePulse     -     � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=�)  #SourceChord.FluentWPF.AcrylicWindow   Enabled$
 True��   AcrylicAccentState$ 
BlurBehind=� � WindowStyle$
 None=�
������  � Title$ CorePulse Desktop��$	��460��$
��1050��$
��1050��$	��460��.��� $
��1050��$	��460�� � ColumnDefinitions �� .+��� -    ��Name$ GPUGlobalUsage��
���   �$��10,120,384,40q����	#FF282828 �� CornerRadius$ 12,12,12,12��	 J� Opacity$		 0.5Q�._�-  'HandyControl.Controls.CircleProgressBar  -   
  Name$
 gpuTemperature_Circle��$��57,57,399,43q����	#FF8500FF   Text$  ��   ArcThickness$ 20Q�   ShowText$ False��$	��200��$	��200�� � ToolTip$ GPU Temperature�� � Maximum$ 85Q��� -   $ GPUGlobalUsage_Copy��
���   �$��671,120,10,40q����	#FF282828$ 12,12,12,12��$		 0.5Q�._�  -   $
 cpuTemperature_Circle��$	��200��$ GPU Temperature��$  ��$ False��$��84,50,85,50q�$	��200�����	#FF8500FF$ 20Q�$ 85Q� � Value$ -2Q��� $��1q����	#FF202020$ 128,128,128,128��$��80,190,795,95q�$	��175��$	��175��._��� -    ��Name$ gpuTemperature_txt��$��90ºC��$��Stretch=�$��Stretch=�
�������$
��Bold"�$��24(�$��	0,60,0,85q�$��	10,0,10,0q��� $��GPU Temp (ºC)��$
��Left=�$��111,267,0,0q�$	��Top=����	#FFC1C1C1$��14(�$��38���� $��GPU Sensor Calculations��$
��Left=�$��
10,121,0,0q�$	��Top=�
�������� -    �Name$ gpuFanSpeed��$
��Left=�$��15��$��338,357,0,0q�$	��Top=�$	��300��$ 95Q����	#FF202020���	#FF8500FF$	��0.1(�$
 3500Q��� $��	Fan Speed��$
��Left=�$��338,312,0,0q�$	��Top=�$��Center=�
�������$��20(�$��0,0,0,0q��� $#��Set maximum value to 3500 RPM��$
��Left=�$��328,330,0,0q�$	��Top=�$��Center=����	#FF969696�� -   $ gpuName_txt��$��NVIDIA RTX 5090 Ti��$
��Left=�
�������$
��Bold"�$��16(�$��328,121,0,0q�$	��Top=�$��44��$	��338��$
��Left=��� $!��CPU Total Core Temperatures��$
��Left=�$��671,121,0,0q�$	��Top=�
��������� $��1q����	#FF202020$ 128,128,128,128��$��768,183,107,102q�$	��175��$	��175��._��� -	   $ cpuTemperature_txt��$��90ºC��$��Stretch=�$��Stretch=�
�������$
��Bold"�$��24(�$��	0,65,0,80q�$��	10,0,10,0q��� $��CPU Temp (ºC)��$
��Left=�$��799,268,0,0q�$	��Top=����	#FFC1C1C1$��14(�$��32��� -
   $ gpuPowerConsumption��$
��Left=�$��15��$��338,293,0,0q�$	��Top=�$	��300��$ 20Q����	#FF202020���	#FF8500FF$	��0.1(�$	 300Q��� $��GPU Power Consumption��$
��Left=�$��338,249,0,0q�$	��Top=�$��Center=�
�������$��20(�$��0,0,0,0q��� $'��!Energy used by your graphics card��$
��Left=�$��328,267,0,0q�$	��Top=�$��Center=����	#FF969696� -   $
 vRAM��$
��Left=�$��16��$��338,230,0,0q�$	��Top=�$	��300��$ 49Q����	#FF202020���	#FF8500FF$	��0.1(�$
 2999Q��� $
��VRAM��$
��Left=�$��338,183,0,0q�$	��Top=�$��Center=�
�������$��20(�$��0,0,0,0q��� $!��Memory utilized by your GPU��$
��Left=�$��328,201,0,0q�$	��Top=�$��Center=����	#FF969696�� -   $ GPUGlobalUsage_Copy4��
���   �$��790,20,10,358q����	#FF282828$ 12,12,12,12��$		 0.5Q��� -   $ GPUGlobalUsage_Copy1��
���   �$��529,20,271,358q����	#FF282828$ 12,12,12,12��$		 0.5Q��� -   $ GPUGlobalUsage_Copy2��
���   �$��269,20,531,358q����	#FF282828$ 12,12,12,12��$		 0.5Q��� -   $ GPUGlobalUsage_Copy3��
���   �$��10,20,790,358q����	#FF282828$ 12,12,12,12��$		 0.5Q��� -   $ GPUGlobalUsage_Copy6��
���   ����	#FF8500FF$ 128,128,128,128��$��25,36,973,374q�._��� $��/Processor.png��$��10,10,10,10q��� -   $ GPUGlobalUsage_Copy5��
���   ����	#FF8500FF$ 128,128,128,128��$��280,36,718,374q�._��� $��/Video Card.png��$��10,10,10,10q��� -   $ GPUGlobalUsage_Copy7��
���   ����	#FF8500FF$ 128,128,128,128��$��538,36,460,374q�._��� $��/Memory Slot.png��$��10,10,10,10q��� -   $ GPUGlobalUsage_Copy8��
���   ����	#FF8500FF$ 128,128,128,128��$��801,36,197,374q�._��� $��/Workstation.png��$��10,10,10,10q��� -   $ cpuUsage_txt��$��Loading��$
��Left=�$��	76,37,0,0q�$	��Top=�
�������$��18(�$
��Bold"�$��35��$	��110��$
��Left=��� -   $ cpu_Usage_txt_Copy��$	��CPU��$
��Left=�$��	77,53,0,0q�$	��Top=����	#FFA0A0A0$��34��$	��110��$
��Left=�$��11(��� -   $ gpu_usage_txt��$��Loading��$
��Left=�$��
331,38,0,0q�$	��Top=�
�������$��18(�$
��Bold"�$��36��$	��110��$
��Left=��� -   $ cpu_Usage_txt_Copy2��$	��GPU��$
��Left=�$��
332,54,0,0q�$	��Top=����	#FFA0A0A0$��34��$	��110��$
��Left=�$��11(��� -   $ ram_usage_txt��$��Loading��$
��Left=�$��
586,37,0,0q�$	��Top=�
�������$��18(�$
��Bold"�$��35��$	��110��$
��Left=��� -   $ cpu_Usage_txt_Copy4��$	��RAM��$
��Left=�$��
586,53,0,0q�$	��Top=����	#FFA0A0A0$��34��$	��110��$
��Left=�$��11(��� -   $ global_usage_txt��$��Loading��$
��Left=�$��
848,37,0,0q�$	��Top=�
�������$��18(�$
��Bold"�$��35��$	��110��$
��Left=��� -   $ cpu_Usage_txt_Copy6��$��GLOBAL USAGE��$
��Left=�$��
848,53,0,0q�$	��Top=����	#FFA0A0A0$��34��$	��110��$
��Left=�$��11(�� -   $ cpuUsage_bar��$
��Left=�$��7��$��	17,95,0,0q�$	��Top=�$	��236�����	#FF202020���	#FF8500FF$	��0.1(�� -   $ gpuUsage_bar��$
��Left=�$��7��$��
277,95,0,0q�$	��Top=�$	��236�����	#FF202020���	#FF8500FF$	��0.1(�� -   $ ramUsage_bar��$
��Left=�$��7��$��
538,95,0,0q�$	��Top=�$	��236�����	#FF202020���	#FF8500FF$	��0.1(�� -   $ globalUsage_bar��$
��Left=�$��7��$��
797,95,0,0q�$	��Top=�$	��236�����	#FF202020���	#FF8500FF$	��0.1(�