internal class MySqlStorage
{
    private object value;
    private MySqlStorageOptions mySqlStorageOptions;

    public MySqlStorage(object value, MySqlStorageOptions mySqlStorageOptions)
    {
        this.value = value;
        this.mySqlStorageOptions = mySqlStorageOptions;
    }
}