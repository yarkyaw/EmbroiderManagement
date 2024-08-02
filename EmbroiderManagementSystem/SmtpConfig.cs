// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.SmtpConfig
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

namespace EmbroiderManagement
{
  public class SmtpConfig
  {
    public string Host { get; set; }

    public int Port { get; set; }

    public bool UseSSL { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }

    public string EmailAddress { get; set; }

    public string Password { get; set; }
  }
}
