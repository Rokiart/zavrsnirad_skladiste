﻿using AutoMapper;

namespace SKladisteAppl.Mappers
{
    public class Mapping<T, DTR, DTI>
    {
        protected Mapper MapperMapReadToDTO;
        protected Mapper MapperMapInsertUpdatedFromDTO;
        protected Mapper MapperMapMapInsertUpdateToDTO;

        public Mapping()
        {
            MapperMapReadToDTO = new Mapper(
                new MapperConfiguration(c =>
                {
                    c.AllowNullDestinationValues = true;
                    //c.AllowNullCollections = true;
                    c.CreateMap<T, DTR>();
                })
                );
            MapperMapInsertUpdatedFromDTO = new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<DTI, T>();
                })
                );

            MapperMapMapInsertUpdateToDTO = new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<T, DTI>();
                })
                );
        }

        public List<DTR> MapReadList(List<T> lista)
        {
            var vrati = new List<DTR>();
            lista.ForEach(e =>
            {
                vrati.Add(MapReadToDTO(e));
            });
            return vrati;
        }

        public DTR MapReadToDTO(T entitet)
        {
            return MapperMapReadToDTO.Map<DTR>(entitet);
        }

        public T MapInsertUpdatedFromDTO(DTI entitet)
        {
            return MapperMapInsertUpdatedFromDTO.Map<T>(entitet);
        }

        public DTI MapInsertUpdateToDTO(T entitet)
        {
            return MapperMapMapInsertUpdateToDTO.Map<DTI>(entitet);
        }
    }
}
