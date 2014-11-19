Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Imports System.IO.Compression

Public Class Form1
    Public currentColor As dye_color = New dye_color("red", "Red", "red", 0, 0, 0)
    Public currentItem As item_info = New item_info("default", "Default", "default type", "default path", {"default qubicle"}, {"default png"}, {"default json"}, "default recipe", "default crafter", "default crafting type")
    Public currentMod As Mod_JSON = New Mod_JSON("", "", "", "", "", "", "", {""}, {New crafter("", {New crafting_type("", "", 0)})})

    Public folder = IO.Path.GetFullPath(".")

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgBar_Rand()
        folder = "C:\Users\David\Desktop\Mock Color mod builder"
        TextBox1.Text = folder
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            CheckBox2.Enabled = True
        Else
            CheckBox2.Enabled = False
        End If
    End Sub

    Private Sub ProgBar_Rand()
        Dim rand As New Random
        VerticalProgressBar1.Value = (rand.Next(0, 100))
        VerticalProgressBar2.Value = (rand.Next(0, 100))
        VerticalProgressBar3.Value = (rand.Next(0, 100))
    End Sub
    Private Sub ProgBar_Rand(ByVal i As Integer)
        Dim rand As New Random
        If i < 2 Then
            VerticalProgressBar1.Value = (rand.Next(0, 100))
        ElseIf i > 2 Then
            VerticalProgressBar1.Value = (rand.Next(0, 100))
            VerticalProgressBar2.Value = (rand.Next(0, 100))
            VerticalProgressBar3.Value = (rand.Next(0, 100))
        Else
            VerticalProgressBar1.Value = (rand.Next(0, 100))
            VerticalProgressBar2.Value = (rand.Next(0, 100))
        End If

    End Sub
    Private Sub ProgBar_100()
        Dim rand As New Random
        VerticalProgressBar1.Value = (100)
        VerticalProgressBar2.Value = (100)
        VerticalProgressBar3.Value = (100)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Text = "Processing..."
        Button1.Enabled = False
        ProgBar_Rand()
        'open mod.json
        Dim JSON_Mod = My.Computer.FileSystem.ReadAllText(folder & "\mod.json")
        'load colors.json
        Dim JSON_Colors = My.Computer.FileSystem.ReadAllText(folder & "\colors.json")
        'check for valid jsons

        'load mod.json into mod_info
        Dim mod_info As Mod_JSON = JsonConvert.DeserializeObject(Of Mod_JSON)(JSON_Mod)
        Dim colors() As dye_color = JsonConvert.DeserializeObject(Of dye_color())(JSON_Colors)
        currentMod = mod_info

        'start manifest aliases
        Dim Manifest_Alias As String = vbTab & """aliases""  : {" & vbNewLine
        Dim aliases As String = ""
        Dim Manifest_Mixinto As String = ""
        Dim manifest_Override As String = ""


        'build crafter types




        'find items
        Dim item_folders() = IO.Directory.GetDirectories(folder & "\entities")
        Dim items(item_folders.Length - 1) As item_info
        For i As Integer = 0 To item_folders.Length - 1
            Console.WriteLine(item_folders(i))
            items(i) = JsonConvert.DeserializeObject(Of item_info)(My.Computer.FileSystem.ReadAllText(item_folders(i) & "\item.json"))
        Next


        'discard old folder
        If IO.Directory.Exists(folder & "\" & mod_info.name) Then My.Computer.FileSystem.DeleteDirectory(folder & "\" & mod_info.name, FileIO.DeleteDirectoryOption.DeleteAllContents)
        'discard old smod
        If IO.File.Exists(folder & "\" & mod_info.name & ".smod") Then IO.File.Delete(folder & "\" & mod_info.name & ".smod")
        'create output folder
        IO.Directory.CreateDirectory(folder & "\" & mod_info.name)


        'loop items
        For i As Integer = 0 To items.Length - 1
            currentItem = items(i)

            'loop colors
            For j As Integer = 0 To colors.Length - 1
                ProgBar_Rand()
                currentColor = colors(j)
                Dim out_folder = folder & "\" & mod_info.name & "\" & PARSE.TEXT(items(i).path).Replace("/", "\")
                Dim out_recipe = folder & "\" & mod_info.name & PARSE.TEXT(mod_info.recipes).Replace("/", "\")

                'make folder
                IO.Directory.CreateDirectory(out_folder)
                If IO.Directory.Exists(out_recipe) = False Then IO.Directory.CreateDirectory(out_recipe)

                'recolor qb
                For k As Integer = 0 To items(i).qb.Length - 1
                    'check that qb and negqb exsist
                    If IO.File.Exists(item_folders(i) & "\" & PARSE.TEXT(items(i).qb(k)) & ".qb") And IO.File.Exists(item_folders(i) & "\" & PARSE.TEXT(items(i).qb(k)) & "_NEG.qb") Then
                        'Console.WriteLine("Yay QB and NEGQB exsist for " & colors(j).name & " " & items(i).name)
                        'load qb and negqb
                        Dim qb As QBModel = QBFile.open(item_folders(i) & "\" & PARSE.TEXT(items(i).qb(k)) & ".qb")
                        Dim nqb As QBModel = QBFile.open(item_folders(i) & "\" & PARSE.TEXT(items(i).qb(k)) & "_NEG.qb")
                        'save recolored qb
                        QBFile.save(color.recolorQB(qb, nqb, colors(j)), out_folder & "\" & PARSE.TEXT(items(i).qb(k)) & "_" & colors(j).name & ".qb")
                    End If
                Next

                'recolor png
                For k As Integer = 0 To items(i).png.Length - 1
                    'check that qb and negqb exsist
                    If IO.File.Exists(item_folders(i) & "\" & PARSE.TEXT(items(i).png(k)) & ".qb") And IO.File.Exists(item_folders(i) & "\" & PARSE.TEXT(items(i).png(k)) & "_NEG.qb") Then
                        'Console.WriteLine("Yay PNG and NEGPNG exsist for " & colors(j).name & " " & items(i).name)
                        'load qb and negqb
                        Dim png As Bitmap = New Bitmap(item_folders(i) & "\" & PARSE.TEXT(items(i).png(k)) & ".png")
                        Dim npng As Bitmap = New Bitmap(item_folders(i) & "\" & PARSE.TEXT(items(i).png(k)) & "_NEG.png")
                        'save recolored qb
                        Dim c_png As Bitmap = color.recolorPNG(png, npng, colors(j))
                        c_png.Save(out_folder & "\" & PARSE.TEXT(items(i).png(k)) & "_" & colors(j).name & ".png", Drawing.Imaging.ImageFormat.Png)
                    End If
                Next

                'parse jsons
                For k As Integer = 0 To items(i).json.Length - 1
                    If IO.File.Exists(item_folders(i) & "\" & PARSE.TEXT(items(i).json(k)) & ".json") Then
                        PARSE.JSON(item_folders(i) & "\" & PARSE.TEXT(items(i).json(k)) & ".json", out_folder & "\" & PARSE.TEXT(items(i).json(k)) & "_" & colors(j).name & ".json")
                    End If
                Next


                'parse recipe
                If IO.File.Exists(item_folders(i) & "\" & items(i).recipe) Then
                    PARSE.JSON(item_folders(i) & "\" & items(i).recipe, out_recipe & "\" & PARSE.TEXT(items(i).name & "_" & colors(j).name & "_recipe.json"))
                End If

                'gen alias
                aliases = aliases & vbTab & vbTab & """" & items(i).name & "_" & colors(j).name & """ : ""file(" & PARSE.TEXT(items(i).path) & items(i).name & "_" & colors(j).name & ")"""
                If i = items.Length - 1 And j = colors.Length - 1 Then
                    aliases = aliases & vbNewLine
                Else
                    aliases = aliases & "," & vbNewLine
                End If

                'gen recipe lists


            Next
        Next

        'write manifest
        ''info
        Dim Manifest_Info As String = "{" & vbNewLine & vbTab & """info"" : {" & vbNewLine & vbTab & vbTab & """name"" : """ & mod_info.name & """" & vbNewLine & vbTab & "}," & vbNewLine
        ''aliases
        'Console.WriteLine(aliases)
        Manifest_Alias = Manifest_Alias & aliases & vbTab & "}"

        ''mixintos
        If IO.File.Exists(folder & "\mixintos.txt") Then
            Manifest_Mixinto = "," & vbNewLine & My.Computer.FileSystem.ReadAllText(folder & "\mixintos.txt")
        End If
        ''overrides
        If IO.File.Exists(folder & "\overrides.txt") Then
            manifest_Override = "," & vbNewLine & My.Computer.FileSystem.ReadAllText(folder & "\overrides.txt")
        End If

        'write
        Dim manifest As String = Manifest_Info & Manifest_Alias & Manifest_Mixinto & manifest_Override & vbNewLine & "}"
        My.Computer.FileSystem.WriteAllText(folder & "\" & mod_info.name & "\manifest.json", manifest, False, Encoding.ASCII)

        'make smod
        If CheckBox1.Checked Then
            ZipFile.CreateFromDirectory(folder & "\" & mod_info.name, folder & "\" & mod_info.name & ".smod", CompressionLevel.NoCompression, True)
        End If

        'delete folder
        If CheckBox2.Checked Then
            My.Computer.FileSystem.DeleteDirectory(folder & "\" & mod_info.name, FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If

        'finished
        ProgBar_100()
        Button1.Text = "Finished!"
        Button1.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FolderBrowserDialog1.ShowDialog()
        TextBox1.Text = FolderBrowserDialog1.SelectedPath
        folder = FolderBrowserDialog1.SelectedPath
    End Sub
End Class

Public Class QBFile
    Public Shared Function open(ByVal inFile As String)
        Dim QB_M As New QBModel
        'Console.WriteLine("Reading QB: " + inFile)

        Using reader As New BinaryReader(File.Open(inFile, FileMode.Open))

            QB_M.setVersion(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte())
            'Console.WriteLine("QB Version: " + QB_M.getVersionAsString())

            QB_M.setColorFormat(reader.ReadInt32)
            'Console.WriteLine("QB Color F: " + QB_M.getColorFormat.ToString)

            QB_M.setZOrient(reader.ReadInt32)
            'Console.WriteLine("QB Z Orien: " + QB_M.getZOrient.ToString)

            QB_M.setCompress(reader.ReadInt32)
            'Console.WriteLine("QB Compres: " + QB_M.getCompress.ToString)

            QB_M.setVisMask(reader.ReadInt32)
            'Console.WriteLine("QB VisMask: " + QB_M.getVisMask.ToString)

            QB_M.setMatrixCount(reader.ReadInt32 - 1)
            'Console.WriteLine("QB #Matrix: " + QB_M.getMatrixCount.ToString)

            'read matrices 
            For i As Integer = 1 To QB_M.getMatrixCount
                'Dim name_length As Byte = reader.ReadByte
                Dim j As Integer = i - 1
                QB_M.matrix(j).setName(reader.ReadString)
                'Console.WriteLine("Matrix Name: " + QB_M.matrix(j).getName)

                QB_M.matrix(j).setMatrixSize(reader.ReadInt32 - 1, reader.ReadInt32 - 1, reader.ReadInt32 - 1)
                'Console.WriteLine("Matrix Size: " + QB_M.matrix(j).getSizeX.ToString + "," + QB_M.matrix(j).getSizeY.ToString + "," + QB_M.matrix(j).getSizeZ.ToString)

                QB_M.matrix(j).setPos(reader.ReadInt32, reader.ReadInt32, reader.ReadInt32)

                If QB_M.getCompress = 0 Then    'if uncompressed
                    For z As Integer = 0 To QB_M.matrix(j).data.GetLength(2) - 1
                        For y As Integer = 0 To QB_M.matrix(j).data.GetLength(1) - 1
                            For x As Integer = 0 To QB_M.matrix(j).data.GetLength(0) - 1
                                QB_M.matrix(j).data(x, y, z) = reader.ReadInt32
                                'Console.WriteLine(x.ToString + "," + y.ToString + "," + z.ToString + ": " + QB_M.matrix(j).data(x, y, z).ToString)
                            Next
                        Next
                    Next
                Else    'if compressed
                    Console.WriteLine("ERROR: Compressed QB, can not process!")
                End If
            Next

        End Using

        Return QB_M
    End Function
    Public Shared Sub save(ByVal out_QB As QBModel, ByVal outFile As String)
        'Console.WriteLine("QB out: " + outFile)
        Using writer As BinaryWriter = New BinaryWriter(File.Open(outFile, FileMode.Create))
            writer.Write(out_QB.getVersion(0))
            writer.Write(out_QB.getVersion(1))
            writer.Write(out_QB.getVersion(2))
            writer.Write(out_QB.getVersion(3))

            writer.Write(out_QB.getColorFormat)

            writer.Write(out_QB.getZOrient)

            writer.Write(out_QB.getCompress)

            writer.Write(out_QB.getVisMask)

            writer.Write(out_QB.getMatrixCount)

            For i As Integer = 1 To out_QB.getMatrixCount
                'Dim name_length As Byte = reader.ReadByte
                Dim j As Integer = i - 1
                writer.Write(out_QB.matrix(j).getName)
                'Console.WriteLine("Matrix Name: " + out_QB.matrix(j).getName)

                writer.Write(out_QB.matrix(j).data.GetLength(0))
                writer.Write(out_QB.matrix(j).data.GetLength(1))
                writer.Write(out_QB.matrix(j).data.GetLength(2))
                'Console.WriteLine("Matrix Size: " + out_QB.matrix(j).getSizeX.ToString + "," + out_QB.matrix(j).getSizeY.ToString + "," + out_QB.matrix(j).getSizeZ.ToString)

                writer.Write(out_QB.matrix(j).getPosX)
                writer.Write(out_QB.matrix(j).getPosY)
                writer.Write(out_QB.matrix(j).getPosZ)

                If out_QB.getCompress = 0 Then    'if uncompressed
                    For z As Integer = 0 To out_QB.matrix(j).data.GetLength(2) - 1
                        For y As Integer = 0 To out_QB.matrix(j).data.GetLength(1) - 1
                            For x As Integer = 0 To out_QB.matrix(j).data.GetLength(0) - 1
                                writer.Write(out_QB.matrix(j).data(x, y, z))
                                'Console.WriteLine(x.ToString + "," + y.ToString + "," + z.ToString + ": " + out_QB.matrix(j).data(x, y, z).ToString)
                            Next
                        Next
                    Next
                Else    'if compressed
                    Console.WriteLine("ERROR: Compressed QB, can not process!")
                End If
            Next
            writer.Dispose()
        End Using
    End Sub
End Class

Public Class QBModel
    Dim Version(3) As Byte
    Dim Color_format As Integer
    Dim z_orient As Integer
    Dim Compress As Integer
    Dim Vis_mask As Integer
    Dim matrix_cnt As Integer   'is this needed?
    Public matrix(0) As QBMatrix

    Sub New()
        Version(0) = 0
        Version(1) = 0
        Version(2) = 0
        Version(3) = 0

        Color_format = 0
        z_orient = 0
        Compress = 0
        Vis_mask = 0
        matrix(0) = New QBMatrix

    End Sub
    Sub New(ByVal v1 As Byte, ByVal v2 As Byte, ByVal v3 As Byte, ByVal v4 As Byte, ByVal cf As Byte, ByVal zo As Byte, ByVal com As Byte, ByVal vm As Byte, ByVal M As QBMatrix())
        Version(0) = v1
        Version(1) = v2
        Version(2) = v3
        Version(3) = v4

        Color_format = cf
        z_orient = zo
        Compress = com
        Vis_mask = vm
        matrix = M

    End Sub
    Sub New(ByVal QB As QBModel)
        Version(0) = QB.Version(0)
        Version(1) = QB.Version(1)
        Version(2) = QB.Version(2)
        Version(3) = QB.Version(3)

        Color_format = QB.Color_format
        z_orient = QB.z_orient
        Compress = QB.Compress
        Vis_mask = QB.Vis_mask
        matrix = QB.matrix
    End Sub

    Public Sub setVersion(ByVal ver1 As Byte, ByVal ver2 As Byte, ByVal ver3 As Byte, ByVal ver4 As Byte)
        Version(0) = ver1
        Version(1) = ver2
        Version(2) = ver3
        Version(3) = ver4
    End Sub
    Public Function getVersion(ByVal i As Byte)
        Return Version(i)
    End Function
    Public Function getVersionAsString()
        Dim ver As String = Version(0).ToString + "." + Version(1).ToString + "." + Version(2).ToString + "." + Version(3).ToString
        Return ver
    End Function

    Public Sub setColorFormat(ByVal CF As Integer)
        Color_format = CF
    End Sub
    Public Function getColorFormat()
        Return Color_format
    End Function

    Public Sub setZOrient(ByVal ZO As Integer)
        z_orient = ZO
    End Sub
    Public Function getZOrient()
        Return z_orient
    End Function

    Public Sub setCompress(ByVal comp As Integer)
        Compress = comp
    End Sub
    Public Function getCompress()
        Return Compress
    End Function

    Public Sub setVisMask(ByVal VisMask As Integer)
        Vis_mask = VisMask
    End Sub
    Public Function getVisMask()
        Return Vis_mask
    End Function

    Public Sub setMatrixCount(ByVal M_CNT As Integer)
        'matrix_cnt = M_CNT
        ReDim Preserve matrix(M_CNT)
    End Sub
    Public Function getMatrixCount()
        'Return matrix_cnt
        Return matrix.GetLength(0)
    End Function

End Class

Public Class QBMatrix
    Dim name As String
    Dim posX As Integer
    Dim posY As Integer
    Dim posZ As Integer
    Public data(,,) As Integer

    Sub New()
        name = ""
        posX = 0
        posY = 0
        posZ = 0
        ReDim data(0, 0, 0)
    End Sub
    Sub New(ByVal m_name As String)
        name = m_name
        posX = 0
        posY = 0
        posZ = 0
        ReDim data(0, 0, 0)
    End Sub
    Sub New(ByVal m_name As String, ByVal pos_x As Integer, ByVal pos_y As Integer, ByVal pos_z As Integer)
        name = m_name
        posX = pos_x
        posY = pos_y
        posZ = pos_z
        ReDim data(0, 0, 0)
    End Sub
    Sub New(ByVal m_name As String, ByVal pos_x As Integer, ByVal pos_y As Integer, ByVal pos_z As Integer, ByVal size_x As Integer, ByVal size_y As Integer, ByVal size_z As Integer)
        name = m_name
        posX = pos_x
        posY = pos_y
        posZ = pos_z
        ReDim data(size_x, size_y, size_z)
    End Sub

    Public Sub setName(ByVal name_str As String)
        name = name_str
    End Sub
    Public Function getName()
        Return name
    End Function

    Public Sub setPosX(ByVal pos As String)
        posX = pos
    End Sub
    Public Function getPosX()
        Return posX
    End Function
    Public Sub setPosY(ByVal pos As String)
        posY = pos
    End Sub
    Public Function getPosY()
        Return posY
    End Function
    Public Sub setPosZ(ByVal pos As String)
        posZ = pos
    End Sub
    Public Function getPosZ()
        Return posZ
    End Function
    Public Sub setPos(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer)
        posX = x
        posY = y
        posZ = z
    End Sub

    Public Sub setMatrixSize(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer)
        ReDim data(x, y, z)
    End Sub

    Public Function getSizeX()
        Return data.GetLength(0)
    End Function
    Public Function getSizeY()
        Return data.GetLength(1)
    End Function
    Public Function getSizeZ()
        Return data.GetLength(2)
    End Function

End Class

Public Class color
    'convert fomr INT32 to RGBA and back
    Public Shared Function INT32toRGBA(ByVal int32 As Integer)
        Dim bytes() As Byte = BitConverter.GetBytes(int32)
        'Console.WriteLine("RGBA color(" + int32.ToString + "): " + bytes(0).ToString + "," + bytes(1).ToString + "," + bytes(2).ToString + "," + bytes(3).ToString)
        Return bytes
    End Function
    Public Shared Function RGBAtoINT32(ByVal r As Integer, ByVal g As Integer, ByVal b As Integer, ByVal a As Integer)
        'convert stuff here
        Dim bytes() As Byte = New Byte() {r, g, b, a}
        Dim int32 As Integer = BitConverter.ToInt32(bytes, 0)
        'Console.WriteLine("Int32 color(" + bytes(0).ToString + "," + bytes(1).ToString + "," + bytes(2).ToString + "," + bytes(3).ToString + "): " + int32.ToString)
        Return int32
    End Function

    'convert RGBA to HSV version 2
    Public Shared Function RGBtoHSV2(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As Integer()
        ''# Normalize the RGB values by scaling them to be between 0 and 1
        Dim red As Decimal = R / 255D
        Dim green As Decimal = G / 255D
        Dim blue As Decimal = B / 255D

        Dim minValue As Decimal = Math.Min(red, Math.Min(green, blue))
        Dim maxValue As Decimal = Math.Max(red, Math.Max(green, blue))
        Dim delta As Decimal = maxValue - minValue

        Dim h As Decimal
        Dim s As Decimal
        Dim v As Decimal = maxValue

        ''# Calculate the hue (in degrees of a circle, between 0 and 360)
        Select Case maxValue
            Case red
                If green >= blue Then
                    If delta = 0 Then
                        h = 0
                    Else
                        h = 60 * (green - blue) / delta
                    End If
                ElseIf green < blue Then
                    h = 60 * (green - blue) / delta + 360
                End If
            Case green
                h = 60 * (blue - red) / delta + 120
            Case blue
                h = 60 * (red - green) / delta + 240
        End Select

        ''# Calculate the saturation (between 0 and 1)
        If maxValue = 0 Then
            s = 0
        Else
            s = 1D - (minValue / maxValue)
        End If

        ''# Scale the saturation and value to a percentage between 0 and 100
        s *= 255
        v *= 255

        ''# Return a color in the new color space
        Return {CInt(Math.Round(h, MidpointRounding.AwayFromZero)), _
                       CInt(Math.Round(s, MidpointRounding.AwayFromZero)), _
                       CInt(Math.Round(v, MidpointRounding.AwayFromZero))}
    End Function
    Public Shared Function HSVtoRGB2(ByVal H As Integer, ByVal S As Integer, ByVal V As Integer) As Byte()
        ''# Scale the Saturation and Value components to be between 0 and 1
        Dim hue As Decimal = H
        Dim sat As Decimal = S / 255D
        Dim val As Decimal = V / 255D

        Dim r As Decimal
        Dim g As Decimal
        Dim b As Decimal

        If sat = 0 Then
            ''# If the saturation is 0, then all colors are the same.
            ''# (This is some flavor of gray.)
            r = val
            g = val
            b = val
        Else
            ''# Calculate the appropriate sector of a 6-part color wheel
            Dim sectorPos As Decimal = hue / 60D
            Dim sectorNumber As Integer = CInt(Math.Floor(sectorPos))

            ''# Get the fractional part of the sector
            ''# (that is, how many degrees into the sector you are)
            Dim fractionalSector As Decimal = sectorPos - sectorNumber

            ''# Calculate values for the three axes of the color
            Dim p As Decimal = val * (1 - sat)
            Dim q As Decimal = val * (1 - (sat * fractionalSector))
            Dim t As Decimal = val * (1 - (sat * (1 - fractionalSector)))

            ''# Assign the fractional colors to red, green, and blue
            ''# components based on the sector the angle is in
            Select Case sectorNumber
                Case 0, 6
                    r = val
                    g = t
                    b = p
                Case 1
                    r = q
                    g = val
                    b = p
                Case 2
                    r = p
                    g = val
                    b = t
                Case 3
                    r = p
                    g = q
                    b = val
                Case 4
                    r = t
                    g = p
                    b = val
                Case 5
                    r = val
                    g = p
                    b = q
            End Select
        End If

        ''# Scale the red, green, and blue values to be between 0 and 255
        r *= 255
        g *= 255
        b *= 255

        ''# Return a color in the new color space
        Return {CInt(Math.Round(r, MidpointRounding.AwayFromZero)), _
                       CInt(Math.Round(g, MidpointRounding.AwayFromZero)), _
                       CInt(Math.Round(b, MidpointRounding.AwayFromZero))}
    End Function

    'TODO: convert from INT32 to HSV and back

    'recolor QB
    Public Shared Function recolorQB(ByVal QB As QBModel, ByVal QB_NEG As QBModel, ByVal dyeColor As dye_color)
        Dim NewQB As New QBModel(recolorQB(QB, QB_NEG, dyeColor.hue, dyeColor.sat, dyeColor.val))
        Return NewQB
    End Function
    Public Shared Function recolorQB(ByVal QB As QBModel, ByVal QB_NEG As QBModel, ByVal hue As Integer, ByVal sat As Integer, ByVal val As Integer) As QBModel
        Dim NewQB As New QBModel(QB)
        'recolor code

        'go through QB_NEG until a black voxel is found
        For i As Integer = 1 To QB_NEG.getMatrixCount
            'Dim name_length As Byte = reader.ReadByte
            Dim j As Integer = i - 1
            For z As Integer = 0 To QB_NEG.matrix(j).data.GetLength(2) - 1
                For y As Integer = 0 To QB_NEG.matrix(j).data.GetLength(1) - 1
                    For x As Integer = 0 To QB_NEG.matrix(j).data.GetLength(0) - 1
                        If QB_NEG.matrix(j).data(x, y, z) > 0 Then
                            Dim colorbytes() As Byte = INT32toRGBA(QB_NEG.matrix(j).data(x, y, z))
                            If colorbytes(0) = 0 And colorbytes(1) = 0 And colorbytes(2) = 0 And colorbytes(3) <> 0 Then    'voxel is visible and pure black
                                'open that voxel in NewQB
                                'convert int32color to rgba, rgba to hsv
                                Dim rgba() As Byte = INT32toRGBA(QB.matrix(j).data(x, y, z))
                                Dim hsv() As Integer = RGBtoHSV2(rgba(0), rgba(1), rgba(2))

                                'change hue
                                hsv(0) = hue
                                'change saturation
                                hsv(1) = hsv(1) + sat
                                'change value
                                hsv(2) = hsv(2) + val

                                'make sure hsv is in the correct bounds
                                If hsv(0) < 0 Then hsv(0) = 0
                                If hsv(0) > 360 Then hsv(0) = 360

                                If hsv(1) < 0 Then hsv(1) = 0
                                If hsv(1) > 255 Then hsv(1) = 255

                                If hsv(2) < 0 Then hsv(2) = 0
                                If hsv(2) > 255 Then hsv(2) = 255

                                'convert new hsv to rgba (using original a)
                                Dim rgba_temp() As Byte = HSVtoRGB2(hsv(0), hsv(1), hsv(2))
                                rgba(0) = rgba_temp(0)
                                rgba(1) = rgba_temp(1)
                                rgba(2) = rgba_temp(2)

                                'convert rbga to int32color
                                'set voxel in NewQB to int32color
                                NewQB.matrix(j).data(x, y, z) = RGBAtoINT32(rgba(0), rgba(1), rgba(2), rgba(3))
                            End If
                        End If
                    Next
                Next
            Next
        Next


        Return NewQB
    End Function

    'recolor PNG
    Public Shared Function recolorPNG(ByVal PNG As Bitmap, ByVal PNG_NEG As Bitmap, ByVal dyeColor As dye_color)
        Dim NewPNG As Bitmap
        NewPNG = recolorPNG(PNG, PNG_NEG, dyeColor.hue, dyeColor.sat, dyeColor.val)
        Return NewPNG
    End Function
    Public Shared Function recolorPNG(ByVal PNG As Bitmap, ByVal PNG_NEG As Bitmap, ByVal hue As Integer, ByVal sat As Integer, ByVal val As Integer)
        Dim NewPNG As New Bitmap(PNG)
        'recolor code here
        For x As Integer = 0 To PNG_NEG.Width - 1
            For y As Integer = 0 To PNG_NEG.Width - 1
                If PNG_NEG.GetPixel(x, y).R = 0 And PNG_NEG.GetPixel(x, y).G = 0 And PNG_NEG.GetPixel(x, y).B = 0 And PNG_NEG.GetPixel(x, y).A <> 0 Then
                    Dim hsv() As Integer = RGBtoHSV2(PNG.GetPixel(x, y).R, PNG.GetPixel(x, y).G, PNG.GetPixel(x, y).B)

                    'change hue
                    hsv(0) = hue
                    'change saturation
                    hsv(1) = hsv(1) + sat
                    'change value
                    hsv(2) = hsv(2) + val

                    'make sure hsv is in the correct bounds
                    If hsv(0) < 0 Then hsv(0) = 0
                    If hsv(0) > 360 Then hsv(0) = 360

                    If hsv(1) < 0 Then hsv(1) = 0
                    If hsv(1) > 255 Then hsv(1) = 255

                    If hsv(2) < 0 Then hsv(2) = 0
                    If hsv(2) > 255 Then hsv(2) = 255

                    Dim rgb() As Byte = HSVtoRGB2(hsv(0), hsv(1), hsv(2))
                    Dim a As Byte = PNG.GetPixel(x, y).A
                    NewPNG.SetPixel(x, y, Drawing.Color.FromArgb(a, rgb(0), rgb(1), rgb(2)))

                End If
            Next
        Next

        Return NewPNG
    End Function
End Class

Public Class dye_color
    Public name As String
    Public hrname As String
    Public rcolor As String
    Public hue As Integer
    Public sat As Integer
    Public val As Integer

    Public Sub New(ByVal nNAME As String, ByVal nHRNAME As String, ByVal nRCOLOR As String, ByVal nHUE As Integer, ByVal nSAT As Integer, ByVal nVAL As Integer)
        name = nNAME
        hrname = nHRNAME
        rcolor = nRCOLOR
        hue = nHUE
        sat = nSAT
        val = nVAL
    End Sub
End Class

Public Class PARSE
    'parses text
    Public Shared Function TEXT(ByVal in_txt As TextBox) As String
        Return TEXT(in_txt.Text)
    End Function
    Public Shared Function TEXT(ByVal in_str As String) As String
        Dim out_str As String = ""

        If in_str.Contains("~") Then
            'parse string
            Dim in_str_split() As String = in_str.Split("~")

            For Each str_sub As String In in_str_split
                If str_sub = "iname" Or str_sub = "name" Then
                    out_str = out_str + Form1.currentItem.name
                ElseIf str_sub = "hriname" Or str_sub = "hrname" Then
                    out_str = out_str + Form1.currentItem.hrname
                ElseIf str_sub = "mname" Then
                    out_str = out_str + Form1.currentMod.name
                ElseIf str_sub = "color" Then
                    out_str = out_str + Form1.currentColor.name
                ElseIf str_sub = "hrcolor" Then
                    out_str = out_str + Form1.currentColor.hrname
                ElseIf str_sub = "rcolor" Then
                    out_str = out_str + Form1.currentColor.rcolor
                ElseIf str_sub = "ifolder" Or str_sub = "impath" Then
                    out_str = out_str + TEXT(Form1.currentItem.path)
                ElseIf str_sub = "rfile" Then
                    out_str = out_str + TEXT(Form1.currentMod.recipes)
                ElseIf str_sub = "crafter" Then
                    out_str = out_str + Form1.currentItem.crafter
                ElseIf str_sub = "type" Then
                    out_str = out_str + Form1.currentItem.type
                ElseIf str_sub = "ctype" Then
                    out_str = out_str + TEXT(Form1.currentItem.craft_type)
                Else
                    out_str = out_str + str_sub
                End If
            Next
        Else
            'string doesn't need parsed
            out_str = in_str
        End If

        'Console.WriteLine("Parsing String: " + in_str)
        'Console.WriteLine("Parsed String: " + out_str)
        Return out_str
    End Function
    Public Shared Function TEXT_D(ByVal in_str As String) As String
        Return TEXT(TEXT(in_str))
    End Function

    'parses and saves JSON files
    Public Shared Sub JSON(ByVal inFile As String, ByVal outFile As String)
        Dim fileReader = My.Computer.FileSystem.OpenTextFileReader(inFile)
        Dim fileWriter = My.Computer.FileSystem.OpenTextFileWriter(outFile, False, Encoding.ASCII)
        While Not fileReader.EndOfStream
            Dim blah = fileReader.ReadLine
            Dim neoBlah = TEXT(blah)
            fileWriter.WriteLine(neoBlah)

            'Console.WriteLine("JSON: " + neoBlah)
        End While

        fileReader.Dispose()
        fileWriter.Dispose()
    End Sub

    'parese custom colors
    Public Shared Function COLOR(ByVal str As String) As dye_color
        Dim split() As String = str.Split(",")
        Return New dye_color(split(0), split(1), split(2), Integer.Parse(split(3)), Integer.Parse(split(4)), Integer.Parse(split(5)))
    End Function

    'parse recipe ingredients
    Public Shared Function RECIP_INGR(ByVal str As String) As String
        Dim newstr As String
        Dim temp() As String = str.Split(",")
        newstr = vbTab & vbTab & "{" & vbCrLf & _
            vbTab & vbTab & vbTab & """material"" : """ & PARSE.TEXT(temp(1)) & """," & vbCrLf & _
            vbTab & vbTab & vbTab & """count"" : " & temp(0) & vbCrLf & _
            vbTab & vbTab & "}"
        Return newstr
    End Function
End Class

Public Class VerticalProgressBar
    Inherits ProgressBar
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.Style = cp.Style Or &H4
            Return cp
        End Get
    End Property
End Class

Public Class Mod_JSON
    Public name As String
    Public author As String
    Public version As String
    Public sh_version As String
    Public url_disc As String
    Public url_git As String
    Public recipes As String
    Public copy_folders() As String
    Public new_crafting_types() As crafter

    Public Sub New(n As String, a As String, v As String, sh_v As String, u_d As String, u_g As String, r As String, c_f() As String, n_c_t() As crafter)
        name = n
        author = a
        version = v
        sh_version = sh_v
        url_disc = u_d
        url_git = u_g
        recipes = r
        copy_folders = c_f
        new_crafting_types = n_c_t
    End Sub
End Class

Public Class mod_info
    Public name As String
    Public author As String
    Public version As String
    Public sh_version As String
    Public url_disc As String
    Public url_git As String
    Public recipes As String
    Public mixintos As String
    Public override As String

    Public Sub New(n As String, a As String, v As String, sh_v As String, u_d As String, u_g As String, r As String, m As String, o As String)
        name = n
        author = a
        version = v
        sh_version = sh_v
        url_disc = u_d
        url_git = u_g
        recipes = r
        mixintos = m
        override = o
    End Sub

End Class

Public Class crafter
    Public hrname As String
    Public crafting_types() As crafting_type

    Public Sub New(h As String, c_t() As crafting_type)
        hrname = h
        crafting_types = c_t
    End Sub
End Class

Public Class crafting_type
    Public name As String
    Public hrname As String
    Public ordinal As Integer

    Public Sub New(n As String, h As String, o As Integer)
        name = n
        hrname = h
        ordinal = o
    End Sub
End Class

Public Class item_info
    Public name As String
    Public hrname As String
    Public type As String
    Public path As String
    Public qb() As String
    Public png() As String
    Public json() As String
    Public recipe As String
    Public crafter As String
    Public craft_type As String

    Public Sub New(n As String, h As String, t As String, pa As String, q() As String, p() As String, j() As String, r As String, c As String, ct As String)
        name = n
        hrname = h
        type = t
        path = pa
        qb = q
        png = p
        json = j
        recipe = r
        crafter = c
        craft_type = ct
    End Sub
End Class