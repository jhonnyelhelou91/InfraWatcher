os_name=`uname`;

#echo "Operating System: $os_name";

# Linux Commands
if [ $os_name == "Linux" ]
then
    serial_number=`lshal | grep system.hardware.serial | awk '{print $3}'`;
    echo "$serial_number";

# AIX Commands
elif [ $os_name == "AIX" ]
then
	serial_number=`lsconf | grep -e "Machine Serial"`;
    echo "$serial_number";

# SunOS Commands
elif [ $os_name == "SunOS" ]
then
	processor_type=`/usr/sbin/psrinfo -pv`
	if [ $processor_type == "X86" ]
	then
		serial_number=`smbios | grep "Serial" | awk '{print $3}'`;
		echo "$serial_number";
	# Add commands here for other processor types...
	fi
# Add commands here for other operating systems...
fi
