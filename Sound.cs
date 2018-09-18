
using System;
using System.Collections.Generic;
using System.IO;
using AI.MathMod.AdditionalFunctions;
using System.Media;

namespace AI.MathMod
{
	/// <summary>
	/// Description of Sound.
	/// </summary>
	public class Sound
	{
		
		public	int chunkID;
		public	int fileSize;
		public	int riffType;
		public	int fmtID;
		public	int fmtSize;
		public	int fmtCode;
		public	int channels;
		public	int sampleRate;
		public	int fmtAvgBPS;
		public	int fmtBlockAlign;
		public	int bitDepth;
		public int dataID;
		public int dataSize;
		
		public Sound()
		{
		}
		
		
		
		public Vector SoundLoad(string path)
		{
				
			Stream waveFileStream = File.OpenRead(path);
			BinaryReader reader = new BinaryReader(waveFileStream);
			
			chunkID = reader.ReadInt32();
			fileSize = reader.ReadInt32();
			riffType = reader.ReadInt32();
			fmtID = reader.ReadInt32();
			fmtSize = reader.ReadInt32();
			fmtCode = reader.ReadInt16();
			channels = reader.ReadInt16();
			sampleRate = reader.ReadInt32();
			fmtAvgBPS = reader.ReadInt32();
			fmtBlockAlign = reader.ReadInt16();
			bitDepth = reader.ReadInt16();

			if (fmtSize == 18)
			{
    			// Read any extra values
    			int fmtExtraSize = reader.ReadInt16();
    			reader.ReadBytes(fmtExtraSize);
			}

			dataID = reader.ReadInt32();
			dataSize = reader.ReadInt32();
			
			List<double> fl = new List<double>();
			
			while(true) {
				try
				{
					fl.Add(reader.ReadInt16()/32000.0);
				}
				catch
				{
					break;
				}
			}
			
			return Vector.ListToVector(fl);
		}
		
		
		
		public void SaveVector(string path, Vector vector, int fd)
		{
			File.Delete(path);
			Stream waveFileStream = File.OpenWrite(path);
			BinaryWriter br = new BinaryWriter(waveFileStream);
			br.Write((Int32)1179011410);
			br.Write((Int32)(2*vector.N+36));
			br.Write((Int32)1163280727);
			br.Write((Int32)544501094);
			br.Write((Int32)16);
			br.Write((Int16)1);
			br.Write((Int16)1);
			br.Write(fd);
			br.Write((Int32)(2*fd));
			br.Write((Int16)2);
			br.Write((Int16)16);
			br.Write((Int32)1635017060);
			br.Write((Int32)(2*vector.N));
			
			double max = Statistic.MaximalValue(MathFunc.abs(vector));
			vector /= max;
			
			for (int i = 0; i < vector.N; i++) {
				br.Write((Int32)(vector[i]*32000));
			}
			
			br.Close();
			
		}
		
		
		
		public void PlayVector(Vector vector, int fd)
		{
			SaveVector("ss", vector, fd);
			SoundPlayer sp = new SoundPlayer("ss");
			sp.Play();
			
			
		}
		
	}
}
